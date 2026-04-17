using System;
using FastEndpoints;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Model;


namespace movie_reservation_system.Features.Movies.AddMovies;

public class AddMovies : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Post("/");
        Roles("Admin");
        Group<MoviesApi>();
    }

    public override async Task HandleAsync(RequestModel req, CancellationToken ct)
    {
        var newMovie = new Model.Movies
        {
            Title= req.Title,
            Description= req.Description,
            Category= req.Category,
            Duration= TimeSpan.Parse(req.Duration)
        };

        await _context.Movies.AddAsync(newMovie, ct);

        await _context.SaveChangesAsync(ct);

        System.Console.WriteLine("Movie added successfully");

        var response = new ResponseModel
        {
            Id= newMovie.Id,
            Message= "Movie added sucessfully!"
        };

        await Send.OkAsync(response, ct);
    }
}
