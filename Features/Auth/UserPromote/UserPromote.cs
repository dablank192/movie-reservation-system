using System;
using FastEndpoints;
using movie_reservation_system.Dto;
using movie_reservation_system.Exception.Auth;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Auth.UserPromote;

public class UserPromote : EndpointWithoutRequest<ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Post("/promote/{userId}");
        Roles("Admin");
        Group<AuthApi>();
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var userId = Route<int>("userId");

        var user = await _context.User.FindAsync(userId, ct)
        ?? throw new UserNotFoundException(userId);

        user.Roles = UserRoles.Admin;

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            UserId= userId,
            Message= "User's role updated successfully"
        };

        await Send.OkAsync(response, ct);
    }
}
