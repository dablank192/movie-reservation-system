using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto;
using movie_reservation_system.Exception.Movies;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Movies.UpdateMovies;

public class UpdateMovies : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}
    public IS3Storage _storage {get; set;}

    public override void Configure ()
    {
        Patch("/{id}");
        Group<MoviesApi>();
        AllowFileUploads();
    }

    public override async Task HandleAsync (RequestModel req, CancellationToken ct)
    {
        var movieId = req.Id;

        var movie = await _context.Movies.FirstOrDefaultAsync(t => t.Id == movieId, ct);

        if (movie == null)
        {
            throw new MoviesNotFoundException();
        }

        string imageUrl = await _storage.UploadImage(req.ImageFile, req.ImageFile.FileName);

        var updatedMovie = new Model.Movies
        {
            Title= req.Title,
            Description= req.Description,
            Category= req.Category,
            Duration= TimeSpan.Parse(req.Duration),
            MovieAvtUrl= imageUrl
        };

        _context.Update(updatedMovie);

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            Id= updatedMovie.Id,
            Message= "Movie updated successfully"
        };

        await Send.OkAsync(response, ct);
    }
}
