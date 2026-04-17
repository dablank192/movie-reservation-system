using System;
using FastEndpoints;

namespace movie_reservation_system.Features.Reservation;

public class ReservationApi : Group
{
    public ReservationApi ()
    {
        Configure("api/v1/reservation", t =>
        {
            t.Description(t => t.WithTags("Reservation Management"));
        });
    }
}
