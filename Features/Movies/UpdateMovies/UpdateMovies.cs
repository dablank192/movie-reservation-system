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
        
        movie.Title= req.Title ?? movie.Title;
        movie.Description= req.Description ?? movie.Description;
        movie.Category= req.Category ?? movie.Category;
        movie.Duration= TimeSpan.Parse(req.Duration);
        movie.MovieAvtUrl= imageUrl ?? movie.MovieAvtUrl;

        _context.Movies.Update(movie);

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            Id= movie.Id,
            Message= "Movie updated successfully"
        };

        await Send.OkAsync(response, ct);
    }
}
