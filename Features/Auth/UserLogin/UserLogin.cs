using System;
using movie_reservation_system.Infrastructure;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Exception;
using System.Security.Authentication;
using movie_reservation_system.Extension;


namespace movie_reservation_system.Features.Auth.UserLogin;

public class UserLogin : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}
    public IUtils Utils {get; set;}

    public override void Configure()
    {
        Post("api/v1/register");
        AllowAnonymous();
    }

    public async Task HandleAsync (JwtAuth auth, RequestModel res, CancellationToken ct)
    {
        var user = await _context.User.FirstOrDefaultAsync(u => u.Email == res.Email, ct);

        if (user == null)
        {
            throw new InvalidCredentialException();
        }

        var password = Utils.VerifyHashedPassword(res.Password, user.HashPassword);

        if (password == false)
        {
            throw new InvalidCredentialException();
        }

        var userToken = await auth.GenerateToken(user);

        var response = new ResponseModel
        {
            Token= userToken
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}
