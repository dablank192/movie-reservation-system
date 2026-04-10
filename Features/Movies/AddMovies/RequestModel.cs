using System;
using movie_reservation_system.Dto;

namespace movie_reservation_system.Features.Movies.AddMovies;

public class RequestModel
{
    public required string Title {get; set;}
    public string? Description {get; set;}
    public MoviesCategory Category {get; set;}
    public required string Duration {get; set;}
}
