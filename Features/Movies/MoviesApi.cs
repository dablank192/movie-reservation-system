using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Movies;

public class MoviesApi : Group
{
    public MoviesApi ()
    {
        Configure("api/v1/movies/admin", ep =>
        {
            ep.Description(t => t.WithTags("Movies Management (Admin)"));
        });
    }
}
