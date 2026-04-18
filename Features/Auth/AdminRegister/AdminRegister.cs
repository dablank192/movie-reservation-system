using System;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto;
using movie_reservation_system.Exception.Auth;
using movie_reservation_system.Extension;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Auth.AdminLogin;

public class AdminRegister : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}
    public IUtils Utils {get; set;}

    public override void Configure()
    {
        Post("/admin");
        Group<AuthApi>();
    }

    public override async Task HandleAsync(RequestModel req, CancellationToken ct)
    {
        var validEmail = await _context.User.FirstOrDefaultAsync(t => t.Email == req.Email);

        if (validEmail != null)
        {
            throw new DublicateEmailException(req.Email);
        }

        var password = Utils.PasswordHasher(req.Password);

        var username = Utils.GenerateRandomUsername();

        var newUser = new Model.User
        {
            Email= req.Email,
            HashPassword= password,
            Username= username,
            Roles= UserRoles.Admin,
        };

        await _context.User.AddAsync(newUser, ct);
        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            Id= newUser.Id,
            Username= username
        };

        await Send.OkAsync(response, ct);
    }
}
