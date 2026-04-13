using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Model;

public class Movies
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public string? Description {get; set;}
    public MoviesCategory? Category {get; set;}
    public TimeSpan? Duration {get; set;}
    public MovieStatus? Status {get; set;}
    public string? MovieAvtUrl {get; set;}

    public List<Showtime> Showtime {get; set;}   
}
