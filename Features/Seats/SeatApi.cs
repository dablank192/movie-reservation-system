using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Seats;

public class SeatApi : Group
{
    public SeatApi()
    {
        Configure("api/v1/seats", t =>
        {
            t.Roles("User");
            t.Description(t => t.WithTags("Seats Management"));
        });
    }
}
