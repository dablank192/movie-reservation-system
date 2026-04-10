using System;

namespace movie_reservation_system.Exception.Movies;

public class MoviesNotFoundException : System.Exception
{
    public MoviesNotFoundException () : base (
        $"Movies not found"
    ) {}
}
