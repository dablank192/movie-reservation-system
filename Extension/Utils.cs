using System;
using Microsoft.AspNetCore.Identity;


namespace movie_reservation_system.Extension;

public class Utils
{
    public string GenerateRandomUsername()
    {
        var randNum = Random.Shared.Next(0, 1000000);

        var randId = randNum.ToString("D8");

        var userId = $"user_{randId}";

        return userId;
    }

    public string PasswordHasher(string originalPassword)
    {
        PasswordHasher<object> hasher = new();

        var hashedPassword = hasher.HashPassword(new object(), originalPassword);

        return hashedPassword;
    }

    public bool VerifyHashedPassword(string originalPassword, string hashedPassword)
    {
        PasswordHasher<object> hasher = new();

        var result = hasher.VerifyHashedPassword(new object(), hashedPassword, originalPassword);

        if (result == PasswordVerificationResult.Failed)
        {
            return false;
        }

        return true;
    }
}
