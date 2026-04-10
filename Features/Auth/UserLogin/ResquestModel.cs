using System;

namespace movie_reservation_system.Features.Auth.UserLogin;

public class RequestModel
{
    public required string Email {get; set;}
    public required string Password {get; set;}
}
