using System;

namespace movie_reservation_system.Exception.Auth;

public class DublicateEmailException : System.Exception
{
    public DublicateEmailException (string email) : base (
        $"Email: {email} already existed!"
    ) {}
}
