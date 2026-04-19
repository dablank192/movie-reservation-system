using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto.Showtime;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Showtime.DeleteShowtime;

public class DeleteShowtime : EndpointWithoutRequest<ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Put("/cancel/{showtimeId}");
        Roles("Admin");
        Group<ShowtimeApi>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var showtimeId = Route<int>("showtimeId");

        var showtime = await _context.Showtime.FirstOrDefaultAsync(t => t.Id == showtimeId, ct)
        ?? throw new ShowtimeNotFoundException(showtimeId);

        showtime?.Status = ShowtimeStatus.Canceled;

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            Message= "Showtime canceled successfully"
        };

        await Send.OkAsync(response, ct);
    }
}
