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
        Get("/");
        AllowAnonymous();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var showtime = await _context.Showtime.ToListAsync(ct);

        List<ResponseModel> result = []; 

        foreach (var show in showtime)
        {
            var response = new ResponseModel
            {
                ShowtimeId= show.Id,
                MovieId= show.MovieId,
                RoomId= show.RoomId,
                StartTime= show.StartTime,
                EndTime= show.EndTime
            };

            result.Add(response);
        }

        await Send.OkAsync(result, ct);
    }
}
