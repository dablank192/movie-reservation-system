using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using movie_reservation_system.Dto;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Exception.Showtime;


namespace movie_reservation_system.Features.Seats.ListSeat;

public class ListSeat : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("/{showtimeId}");
        Group<SeatApi>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var showtimeId = Route<int>("showtimeId");

        var checkShowtime = await _context.Showtime.FindAsync(showtimeId, ct) ?? throw new ShowtimeNotFoundException(showtimeId);

        var listEmptySeat = await _context.Seats.Where(seat => seat.RoomId == showtimeId)
        .Select(seat => new ResponseModel
        {
            Id= seat.Id,
            RoomId= seat.RoomId,
            Row= seat.Row,
            Number= seat.Number,
            Type= seat.Type,
            IsBooked= _context.ReservationSeats.Any(rseat =>
            rseat.SeatId == seat.Id &&
            rseat.Reservations!.ShowtimeId == showtimeId &&
            rseat.Reservations.Status != ReservationStatus.Canceled)
        })
        .OrderBy(t => t.Row)
        .ThenBy(t => t.Number)
        .ToListAsync(ct);

        await Send.OkAsync(listEmptySeat, ct);
    }
}
