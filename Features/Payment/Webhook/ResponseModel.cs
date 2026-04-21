using System;

namespace movie_reservation_system.Features.Payment.Webhook;

public class ResponseModel
{
    public bool Success {get; set;}
    public string? Message {get; set;}
}
