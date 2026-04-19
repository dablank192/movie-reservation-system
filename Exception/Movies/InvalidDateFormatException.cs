using System;

namespace movie_reservation_system.Exception.Movies;

public class InvalidDateFormatException : System.Exception
{
    public InvalidDateFormatException() : base (
        $"Invalid date format, format must be 'yyyy-MM-dd'."
    ) {}
}
