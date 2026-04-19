using System;
using movie_reservation_system.Dto.Showtime;

namespace movie_reservation_system.Model;

public class Showtime
{
    public int Id {get; set;}
    public int MovieId {get; set;}
    public int RoomId {get; set;}
    public ShowtimeStatus Status {get; set;}
    public DateTime StartTime {get; set;}
    public DateTime EndTime {get; set;}

    public Movies? Movies {get; set;}
    public Rooms? Rooms {get; set;}
    public List<ReservationSeats> ReservationSeats {get; set;} 
    public List<Reservations> Reservations {get; set;}
}
