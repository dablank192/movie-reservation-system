using System;
using movie_reservation_system.Dto;
using System.Security.Claims;
using FastEndpoints;
using movie_reservation_system.Exception.Seats;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Model;
using Microsoft.EntityFrameworkCore;


namespace movie_reservation_system.Features.Seats.BookSeat;

public class BookSeat : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Post("/book");
        Roles("User", "Admin");
        Group<SeatApi>();
    }

    public override async Task HandleAsync (RequestModel req, CancellationToken ct)
    {
        int userId;
        
        decimal totalPrice = 0;

        List<SeatDto> Seats = [];

        List<ReservationSeats> reservationList = [];

        var userIdString = this.User.FindFirstValue("UserId");

        if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out userId))
        {
            await Send.ErrorsAsync(401, ct);
            return;
        }

        var showtime = await _context.Showtime.FindAsync(req.ShowtimeId, ct)
        ?? throw new ShowtimeNotFoundException(req.ShowtimeId);
    
        var bookedSeatShowtime = await _context.ReservationSeats
        .Where(rs => rs.Reservations!.ShowtimeId == showtime.Id)
        .Select(rs => rs.SeatId)
        .ToListAsync(ct);

        var overlappingSeat = req.SeatId!
        .Intersect(bookedSeatShowtime)
        .ToList();

        if (overlappingSeat.Any())
        {
            throw new UsedSeatException();
        }

        var validSeats = await _context.Seats
        .Where(s => req.SeatId!.Contains(s.Id) && s.RoomId == showtime.RoomId)
        .ToListAsync(ct);

        if (validSeats.Count() != req.SeatId!.Count())
        {
            throw new InvalidSeatException();
        }

        foreach (var seat in validSeats)
        {
            int price = seat.Type == SeatsType.Vip ? (int)SeatPrice.Vip : (int)SeatPrice.Normal;

            totalPrice += price;

            reservationList.Add(new ReservationSeats
            {
                ShowtimeId= req.ShowtimeId,
                SeatId = seat.Id,
                PriceAtBooking = price
            });

            Seats.Add(new SeatDto
            {
                Id= seat.Id,
                Row= seat.Row,
                Number= seat.Number,
                Type= seat.Type
            });
        }

        var newReservation = new Reservations
        {
            UserId= userId,
            ShowtimeId= req.ShowtimeId,
            TotalAmount= totalPrice,
            Status= ReservationStatus.Pending,
            ReservationsSeats = reservationList
        };

        try
        {
            await _context.Reservations.AddAsync(newReservation, ct);

            await _context.SaveChangesAsync(ct);
        }

        catch(DbUpdateException)
        {
            throw new UsedSeatException();
        }

        var response = new ResponseModel
        {
            MovieId= showtime.MovieId,
            RoomId= showtime.RoomId,
            StartTime= showtime.StartTime,
            Seats= Seats
        };

        
        await Send.OkAsync(response, ct);
    }
}


