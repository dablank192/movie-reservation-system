using System;
using FluentValidation;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Auth.UserLogin;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator ()
    {
        RuleFor(x => x.Email)
        .EmailAddress()
        .NotEmpty()
        .WithMessage("Email field must not be empty");

        RuleFor(x => x.Password)
        .NotEmpty()
        .WithMessage("Password must not be empty")
        .MinimumLength(8)
        .WithMessage("Password must be 8 characters minimum");
    } 
}
