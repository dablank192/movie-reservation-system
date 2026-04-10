using System;

namespace movie_reservation_system.Exception.Auth;

public class InvalidCredentialsException : System.Exception
{
    public InvalidCredentialsException () : base (
        $"Wrong username or password"
    ) {}
}
