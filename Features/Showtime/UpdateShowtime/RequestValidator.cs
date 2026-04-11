using System;
using FluentValidation;


namespace movie_reservation_system.Features.Showtime.UpdateShowtime;

public class RequestValidator : AbstractValidator<RequestModel>
{
    public RequestValidator ()
    {
        RuleFor(t => t.StartTime)
        .Must(t => t > DateTime.MinValue).WithMessage("Invalid format");
    }
}