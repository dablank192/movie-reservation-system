using System;
using Ardalis.Specification.EntityFrameworkCore;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Movies.ListMovies;

public class ListMovie : EndpointWithoutRequest<List<ResponseModel>>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Get("/");
        Group<MoviesApi>();
        AllowAnonymous();
    }

    public override async Task HandleAsync (CancellationToken ct)
    {
        var movies = await _context.Movies.Where(t => t.Status == MovieStatus.NowShowing)
        .Select(t => new ResponseModel
        {
            MovieId= t.Id,
            MovieTitle= t.Title,
            Category= t.Category,
            Duration= t.Duration,
            Status= t.Status,
            MovieAvtUrl= t.MovieAvtUrl
        }).ToListAsync(ct);

        await Send.OkAsync(movies, ct);
    }
}
