using System;
using FastEndpoints;
using movie_reservation_system.Extension;
using movie_reservation_system.Infrastructure;
using movie_reservation_system.Exception.Auth;
using Microsoft.EntityFrameworkCore;


namespace movie_reservation_system.Features.User;

public class UserRegister : Endpoint<UserRequestModel, UserResponseModel>
{
    public AppDbContext Context {get; set;}
    public IUtils Utils {get; set;}

    public override void Configure()
    {
        Post("api/v1/auth");
        AllowAnonymous();
    }

    public override async Task HandleAsync (UserRequestModel req, CancellationToken ct)
    {
        var getDublicateEmail = await Context.User.FirstOrDefaultAsync(u => u.Email == req.Email, ct);

        if (getDublicateEmail != null) 
        {
            throw new DublicateEmailException(req.Email);
        }

        var hashedPassword = Utils.PasswordHasher(req.Password);

        var username = Utils.GenerateRandomUsername();

        var newUser = new Model.User
        {
            Email = req.Email,
            Username = username,
            HashPassword = hashedPassword
        };

        await Context.AddAsync(newUser, ct);
        await Context.SaveChangesAsync(ct);

        
        var response = new UserResponseModel
        {
            Id = newUser.Id,
            Username = username
        };

        await Send.OkAsync(response, cancellation: ct);
    }
}
