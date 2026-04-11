using System;

namespace movie_reservation_system.Features.Showtime.AddShowtime;

public class RequestModel
{
    public int MovieId {get; set;}
    public int RoomId {get; set;}
    public DateTime StartTime {get; set;}
}   
