using System;

namespace movie_reservation_system.Dto.Reservations;

public class ReservationDto
{
    public string? MovieTitle {get; set;}
    public int RoomId {get; set;}
    public int SeatId {get; set;}
    public DateTime StartTime {get; set;}
}
