using System;

namespace movie_reservation_system.Dto;

public class ShowtimeDto
{
    public int Id {get; set;}
    public int MovieId {get; set;}
    public int RoomId {get; set;}
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}
}
