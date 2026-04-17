using System;
using FastEndpoints;


namespace movie_reservation_system.Features.Showtime;

public class ShowtimeApi : Group
{
    public ShowtimeApi ()
    {
        Configure("api/v1/showtime", t =>
        {
            t.Description(t => t.WithTags("Showtimes Management (Admin)"));
        });
    }
}
