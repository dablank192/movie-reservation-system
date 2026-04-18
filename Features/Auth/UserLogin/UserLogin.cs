using System;
using movie_reservation_system.Infrastructure;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using FastEndpoints.Security;
using System.Security.Authentication;
using movie_reservation_system.Extension;


namespace movie_reservation_system.Features.Auth.UserLogin;

public class UserLogin : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}
    public IUtils Utils {get; set;}
    public IConfiguration _config {get; set;}

    public override void Configure()
    {
        Post("/login");
        Group<AuthApi>();
        AllowAnonymous();
    }

    public override async Task HandleAsync (RequestModel res, CancellationToken ct)
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

        var userToken = JwtBearer.CreateToken(option =>
        {
            option.SigningKey= _config["Jwt:Key"]!;
            option.ExpireAt= DateTime.UtcNow.AddHours(1);
            option.User.Claims.Add(("UserId", user.Id.ToString()));
            option.User.Roles.Add(user.Roles.ToString());
        });

        var response = new ResponseModel
        {
            Id= user.Id,
            Token= userToken
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}
