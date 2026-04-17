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
        decimal totalPrice = 0;

        List<SeatDto> Seats = [];

        List<ReservationSeats> reservationList = [];

        var userIdString = this.User.FindFirstValue("UserId");

        int userId;

        var toInt = int.TryParse(userIdString, out userId);

        var showtime = await _context.Showtime.FindAsync(req.ShowtimeId, ct) ?? throw new ShowtimeNotFoundException(req.ShowtimeId);
    

        foreach (var seat in req.SeatId!)
        {
            var bookedSeat = _context.Seats.Any(s => s.Id == seat &&
            s.RoomId == showtime.RoomId &&
            s.ReservationSeats.Any(t => t.SeatId == seat));

            if (bookedSeat == true)
            {
                throw new InvalidSeatException();
            }
        }

        foreach (var seat in req.SeatId)
        {
            var seats = await _context.Seats.FindAsync(seat, ct);

            if (seats == null)
            {
                throw new InvalidSeatException();
            }

            int price = seats.Type == SeatsType.Vip ? (int)SeatPrice.Vip : (int)SeatPrice.Normal;

            totalPrice += price;

            reservationList.Add(new ReservationSeats
            {
                SeatId = seat,
                PriceAtBooking = price
            });

            Seats.Add(new SeatDto
            {
                Id= seat,
                Row= seats.Row,
                Number= seats.Number,
                Type= seats.Type
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


