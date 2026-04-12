using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Model;


namespace movie_reservation_system.Features.Movies.SortListMovie;

public class ListSortMovie : Endpoint<RequestModel, List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("api/v1/movies");
        AllowAnonymous();
    }

    public override async Task HandleAsync (RequestModel req, CancellationToken ct)
    {
        DateTime startDate = req.StartDate.Date;
        DateTime endDate = req.StartDate.AddDays(1);

        var movies = await _context.Movies
        .Where(m => m.Showtime.Any(s => s.StartTime >= startDate && s.StartTime < endDate))
        .Include(st => st.Showtime.Where(st => st.StartTime >= startDate && st.StartTime < endDate))
        .Select(t => new ResponseModel
        {
            MovieId= t.Id,
            MovieTitle= t.Title,
            MovieAvtUrl= t.MovieAvtUrl,
            Showtimes= t.Showtime.Select(s => new ShowtimeDto
            {
                Id= s.Id,
                MovieId= s.MovieId,
                RoomId= s.RoomId,
                StartTime= s.StartTime,
                EndTime= s.EndTime,
            }).ToList()
        }).ToListAsync(ct);

        await Send.OkAsync(movies, ct);
    }
}
