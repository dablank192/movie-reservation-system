using System;

namespace movie_reservation_system.Exception.Showtime;

public class ShowtimeNotFoundException : System.Exception
{
    public ShowtimeNotFoundException(int showId) : base (
        $"Showtime {showId} not found"
    ) {}
}
