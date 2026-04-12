using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Features.Movies.SortListMovie;

public class ResponseModel
{
    public int MovieId {get; set;}
    public string? MovieTitle {get; set;}
    public string? MovieAvtUrl {get; set;}
    public List<ShowtimeDto>? Showtimes {get; set;}
}
