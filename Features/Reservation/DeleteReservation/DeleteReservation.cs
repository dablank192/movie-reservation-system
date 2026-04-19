using System;
using System.Security.Claims;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Reservation.DeleteReservation;

public class DeleteReservation : EndpointWithoutRequest<ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Put("/{reservationId}");
        Roles("User", "Admin");
        Group<ReservationApi>();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var userIdString = this.User.FindFirstValue("UserId");

        int userId;

        var toInt = int.TryParse(userIdString, out userId);

        int reservationId = Route<int>("reservationId");

        DateTime timeNow = DateTime.UtcNow;

        var getReservation = await _context.Reservations.FirstOrDefaultAsync(
            r => r.UserId == userId &&
            r.Id == reservationId &&
            r.Showtime!.StartTime > timeNow, ct)
        ?? throw new ReservationNotFoundException();

        getReservation.Status = ReservationStatus.Canceled;

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            Message= $"Reservation {reservationId} deleted successfully"
        };

        await Send.OkAsync(response, ct);
    }
}
