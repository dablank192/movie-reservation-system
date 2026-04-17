using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Exception.Movies;
using movie_reservation_system.Features.Movies.DeleteMovies;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Movies.DeleteMovies;

public class DeleteMovies : EndpointWithoutRequest
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Delete("/{id}");
        Roles("Admin");
        Group<MoviesApi>();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        int movieId = Route<int>("id");

        var movie = await _context.Movies.FirstOrDefaultAsync(t => t.Id == movieId, ct);

        if (movie == null)
        {
            throw new MoviesNotFoundException();
        }

        _context.Remove(movie);
        await _context.SaveChangesAsync(ct);

        System.Console.WriteLine("Movie deleted successfully");

        await Send.NoContentAsync(ct);
    }
}

