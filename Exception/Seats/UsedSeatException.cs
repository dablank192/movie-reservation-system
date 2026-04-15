using System;

namespace movie_reservation_system.Exception.Seats;

public class UsedSeatException : System.Exception
{
    public UsedSeatException() : base (
        $"Seat have been reserved by other user"
    ) {}
}
