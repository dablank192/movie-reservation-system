using System;
using FastEndpoints;


namespace movie_reservation_system.Features.Payment;

public class PaymentApi : Group
{
    public PaymentApi()
    {
        Configure("api/v1/payment", t =>
        {
            t.Description(t => t.WithTags("Payment Management"));
        });
    }
}
