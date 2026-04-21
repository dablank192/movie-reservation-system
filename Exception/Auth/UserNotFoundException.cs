using System;

namespace movie_reservation_system.Exception.Auth;

public class UserNotFoundException : System.Exception
{
    public UserNotFoundException(int userId) : base (
        $"User {userId} not found"
    )
    {
        
    }
}
