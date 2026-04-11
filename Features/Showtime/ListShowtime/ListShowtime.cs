using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Showtime.ListShowtime;

public class ListShowtime : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("api/v1/showtime");
        AllowAnonymous();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var showtime = await _context.Showtime.Select(t => new ResponseModel
        {
            ShowtimeId= t.Id,
            MovieTitle= t.Movies!.Title,
            RoomId= t.RoomId,
            StartTime= t.StartTime,
            EndTime= t.EndTime
        }).OrderByDescending(t => t.StartTime).ToListAsync(ct);

        await Send.OkAsync(showtime, ct);
    }
}
