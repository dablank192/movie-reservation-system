using System;

namespace movie_reservation_system.Features.User;

public class UserRequestModel
{
    public required string Email {get; set;}
    public required string Password {get; set;}
}