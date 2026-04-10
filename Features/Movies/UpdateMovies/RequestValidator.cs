using System;
using FluentValidation;

namespace movie_reservation_system.Features.Movies.UpdateMovies;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator ()
    {
        RuleFor(x => x.Title)
        .MaximumLength(50)
        .When(x => x.Title != null);

        RuleFor(x => x.Description)
        .MaximumLength(500)
        .When(x => x.Description != null);

        RuleFor(x => x.Category)
        .IsInEnum().WithMessage("Invali Category")
        .When(x => x.Category != null);

        RuleFor(x => x.Duration)
        .Must(t => TimeSpan.TryParse(t, out _)).WithMessage("Invalid Format")
        .When(x => x.Duration != null);

        RuleFor(x => x.ImageFile)
        .Must(t => !t.ContentType.Contains("jpeg") || !t.ContentType.Contains("png"))
        .WithMessage("Invalid File Format")
        .When(x => x.ImageFile != null);

        RuleFor(x => x)
        .Must(x => x.Title != null || x.Description != null || x.Duration != null || x.Category != null || x.ImageFile != null)
        .WithMessage("Need at least one field to update");
    }
}
