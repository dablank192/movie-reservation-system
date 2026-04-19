using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Movies;

public class MoviesApiUser : Group
{
    public MoviesApiUser ()
    {
        Configure("api/v1/movies", t =>
        {
            t.Description(t => t.WithTags("Movies (User)"));
        });
    }
}
