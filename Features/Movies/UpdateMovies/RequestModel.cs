using System;
using FastEndpoints;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Movies.UpdateMovies;

public class RequestModel
{
    [BindFrom("id")]
    public int Id {get; set;}

    public required string Title {get; set;}
    public string? Description {get; set;}
    public MoviesCategory? Category {get; set;}
    public string? Duration {get; set;}
    public IFormFile? ImageFile {get; set;}
}
