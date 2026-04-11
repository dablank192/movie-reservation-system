using System;
using FluentValidation;

namespace movie_reservation_system.Features.Showtime.AddShowtime;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator ()
    {
        RuleFor(t => t.StartTime)
        .NotEmpty().WithMessage("Start time is required")
        .Must(t => t > DateTime.MinValue).WithMessage("Invalid format");

        RuleFor(t => t.MovieId)
        .NotEmpty().WithMessage("Movie is required");

        RuleFor(t => t.RoomId)
        .NotEmpty().WithMessage("Room is required");
    }
}
