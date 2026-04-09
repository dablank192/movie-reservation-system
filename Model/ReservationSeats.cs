using System;

namespace movie_reservation_system.Model;

public class ReservationSeats
{
    public int Id {get; set;}
    public int ReservationsId {get; set;}
    public int SeatId {get; set;}
    public decimal PricaAtBooking {get; set;}

    public Reservations? Reservations {get; set;}
    public Seats? Seats {get; set;}
}
