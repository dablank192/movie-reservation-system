using System;
using FastEndpoints;
using movie_reservation_system.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace movie_reservation_system.Features.Movies.ListMovies;

public class ListMovieAdmin : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("/");
        Roles("Admin");
        Group<MoviesApi>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var movies = await _context.Movies
        .Select(t => new ResponseModel
        {
            MovieId= t.Id,
            MovieTitle= t.Title,
            Category= t.Category,
            Duration= t.Duration,
            Status= t.Status,
            MovieAvtUrl= t.MovieAvtUrl
        })
        .ToListAsync(ct);

        await Send.OkAsync(movies, ct);
    }
}
