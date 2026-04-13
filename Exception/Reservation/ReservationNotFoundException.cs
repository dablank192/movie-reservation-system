using System;

namespace movie_reservation_system.Exception.Reservation;

public class ReservationNotFoundException : System.Exception
{
    public ReservationNotFoundException () : base (
        $"Chosen reservation not found"
    ) {}
}
