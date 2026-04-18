using System;

namespace movie_reservation_system.Features.Auth.AdminLogin;

public class RequestModel
{
    public required string Email {get; set;}
    public required string Password {get; set;}
}

