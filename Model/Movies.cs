using System;

namespace movie_reservation_system.Model;

public class Movies
{
    public int Id {get; set;}
    public required string Title {get; set;}
    public string? Description {get; set;}
    public int Duration {get; set;}

    public List<Showtime> Showtime {get; set;}   
}
