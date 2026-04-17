using System;
using System.Security.Claims;
using FastEndpoints;
using FastEndpoints.Security;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto.Reservations;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Exception.Seats;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Model;

namespace movie_reservation_system.Features.Reservation.ListReservation;

public class Reservation : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("/");
        Roles("User", "Admin");
        Group<ReservationApi>();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var userIdString = this.User.FindFirstValue("UserId");

        int userId;

        var toInt = int.TryParse(userIdString, out userId);

        var userReservation = await _context.Reservations
        .Where(r => r.UserId == userId)
        .Include(r => r.Showtime)
            .ThenInclude(r => r.Movies)
        .Include(r => r.ReservationsSeats)
        .ToListAsync(ct);

        var response = userReservation.Select(t => new ResponseModel
        {
            Id= t.Id,
            UserId= t.UserId,
            TotalAmount= t.TotalAmount,
            Status= t.Status,

            AllReservations= t.ReservationsSeats.Select(rs => new ReservationDto
            {
                MovieTitle= t.Showtime!.Movies!.Title,
                RoomId= t.Showtime.RoomId,
                SeatId= rs!.SeatId,
                StartTime= t.Showtime.StartTime
            }).ToList()
        }).ToList();

        await Send.OkAsync(response, ct);
    }
}

