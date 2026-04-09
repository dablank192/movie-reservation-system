using System;

namespace movie_reservation_system.Model;

public class Rooms
{
    public int Id {get; set;}
    public required string Name {get; set;}
    public int TotalCapacity {get; set;}

    public List<Seats> Seats {get; set;}
    public List<Showtime> Showtime {get; set;}
}
