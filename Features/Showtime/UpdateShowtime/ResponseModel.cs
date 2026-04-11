using System;

namespace movie_reservation_system.Features.Showtime.UpdateShowtime;

public class ResponseModel
{
    public int ShowtimeId {get; set;}
    public int MovieId {get; set;}
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}
}
