using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Model;

public class Seats
{
    public int Id {get; set;}
    public int RoomId {get; set;}
    public required string Row {get; set;}
    public int Number {get; set;}
    public required SeatsType Type {get; set;} = SeatsType.Normal;

    public Rooms? Rooms {get; set;}
    public List<ReservationSeats> ReservationSeats {get; set;}
}