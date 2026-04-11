using System;

namespace movie_reservation_system.Exception.Showtime;

public class ShowtimeOverlappedException : System.Exception
{
    public ShowtimeOverlappedException(int movieId) : base (
        $"Showtime for movie {movieId} is already taken"
    ) {}
}
