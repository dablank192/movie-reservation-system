using System;
using movie_reservation_system.Dto;


namespace movie_reservation_system.Model;

public class Reservations
{
    public int Id {get; set;}
    public int UserId {get; set;}
    public int ShowtimeId {get; set;}
    public decimal TotalAmount {get; set;}
    public DateTime ExpiredAt {get; set;}
    public ReservationStatus Status {get; set;} = ReservationStatus.Pending;

    public User? User {get; set;}
    public Showtime? Showtime {get; set;}
    public List<ReservationSeats> ReservationsSeats {get; set;}
}
