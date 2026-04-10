using System;
using FastEndpoints;
using FluentValidation;


namespace movie_reservation_system.Features.Movies.AddMovies;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator ()
    {
        RuleFor(x => x.Title)
        .NotEmpty().WithMessage("Title must not be empty")
        .MaximumLength(50);

        RuleFor(x => x.Description)
        .NotEmpty().WithMessage("Description must not be empty")
        .MaximumLength(500);

        RuleFor(x => x.Category)
        .IsInEnum().WithMessage("Invali Category");

        RuleFor(x => x.Duration)
        .NotEmpty().WithMessage("Duration must not be empty")
        .Must(t => TimeSpan.TryParse(t, out _)).WithMessage("Invalid Format");
    } 
}
