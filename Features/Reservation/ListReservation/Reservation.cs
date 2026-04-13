using System;
using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto.Reservations;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Exception.Seats;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Infrastructure;

namespace movie_reservation_system.Features.Reservation.ListReservation;

public class Reservation : EndpointWithoutRequest<ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("/");
        Group<ReservationApi>();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        int userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

        var reservation = await _context.Reservations.FirstOrDefaultAsync(r => r.UserId == userId, ct)
            ?? throw new ReservationNotFoundException();

        var showtime = await _context.Showtime.FindAsync(reservation!.ShowtimeId, ct)
            ?? throw new ShowtimeNotFoundException(reservation.ShowtimeId);
    
        var seat = await _context.ReservationSeats.FirstOrDefaultAsync(rs => rs.ReservationsId == reservation.Id, ct)
            ?? throw new InvalidSeatException();

        var response = new ResponseModel
        {
            Id= reservation.Id,
            UserId= reservation.UserId,
            TotalAmount= reservation.TotalAmount,
            Status= reservation.Status,

            AllReservations= new List<ReservationDto>
            {
                new ReservationDto
                {
                    MovieTitle= showtime.Movies!.Title,
                    RoomId= showtime.RoomId,
                    SeatId= seat!.SeatId,
                    StartTime= showtime.StartTime
                }
            }
        };

        await Send.OkAsync(response, ct);
    }
}

