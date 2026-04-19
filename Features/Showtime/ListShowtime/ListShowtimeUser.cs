using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto.Showtime;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Showtime.ListShowtime;

public class ListShowtimeUser : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("api/v1/showtime");
        Description(t => t.WithTags("Showtime (User)"));
        Roles("User", "Admin");
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var showtime = await _context.Showtime
        .Where(s => s.Status == ShowtimeStatus.Opened)
        .Select(s => new ResponseModel
        {
            ShowtimeId= s.Id,
            MovieTitle= s.Movies!.Title,
            RoomId= s.RoomId,
            StartTime= s.StartTime,
            EndTime= s.EndTime 
        })
        .ToListAsync(ct);

        await Send.OkAsync(showtime, ct);
    }
}
