using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Movies.ListMovies;

public class ResponseModel
{
    public int MovieId {get; set}
    public string? MovieTitle {get; set;}
    public MoviesCategory? Category {get; set;}
    public TimeSpan? Duration {get; set;}
    public string? MovieAvtUrl {get; set;}
}
