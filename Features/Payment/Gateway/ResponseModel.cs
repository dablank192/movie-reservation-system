using System;

namespace movie_reservation_system.Features.Payment.Gateway;

public class ResponseModel
{
    public string? PaymentUrl {get; set;}
    public decimal Amount {get; set;}
    public string? Message {get; set;}
}
