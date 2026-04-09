using System;

namespace movie_reservation_system.Extension;

public interface IUtils
{
    public string GenerateRandomUsername();

    public string PasswordHasher(string originalPassword);

    public bool VerifyHashedPassword(string originalPassword, string hashedPassword);
}
