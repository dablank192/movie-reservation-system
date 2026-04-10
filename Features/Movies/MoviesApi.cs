using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Movies;

public class MoviesApi : Group
{
    public MoviesApi ()
    {
        Configure("api/v1/movies", ep =>
        {
            ep.Roles("Admin");
            ep.Description(t => t.WithTags("Movies Management (Admin)"));
        });
    }
}
