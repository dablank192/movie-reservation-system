using System;

namespace movie_reservation_system.Exception.Seats;

public class InvalidSeatException : System.Exception
{
    public InvalidSeatException() : base (
        $"Seats not found"
    ) {}
}
