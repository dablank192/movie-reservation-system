using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using movie_reservation_system.Exception;
using movie_reservation_system.Exception.Auth;
using movie_reservation_system.Exception.Movies;
using movie_reservation_system.Exception.Reservation;
using movie_reservation_system.Exception.Seats;
using movie_reservation_system.Exception.Showtime;


namespace movie_reservation_system.Exception;

public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler (ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync (
        HttpContext httpContext,
        System.Exception exception,
        CancellationToken ct
    )
    {
        _logger.LogError(exception, exception.Message);

        var problemDetails = new ProblemDetails
        {
            Instance= httpContext.Request.Path
        };

        
        if (exception is DublicateEmailException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Email existed";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status401Unauthorized;
        }

        else if (exception is InvalidCredentialsException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status401Unauthorized;
            problemDetails.Title = "Wrong username or password";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status401Unauthorized;
        }

        else if (exception is InvalidDateFormatException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            problemDetails.Title = "Invalid date format";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status400BadRequest;
        }

        else if (exception is MoviesNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            problemDetails.Title = "Movie not existed";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status404NotFound;
        }

        else if (exception is ReservationNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            problemDetails.Title = "Reservation not existed";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status404NotFound;
        }

        else if (exception is InvalidSeatException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            problemDetails.Title = "Seat not existed";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status404NotFound;
        }

        else if (exception is UsedSeatException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            problemDetails.Title = "Seat was already taken";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status409Conflict;
        }

        else if (exception is ShowtimeNotFoundException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            problemDetails.Title = "Showtime not existed";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status404NotFound;
        }

        else if (exception is ShowtimeOverlappedException)
        {
            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
            problemDetails.Title = "Showtime's date is overlapped";
            problemDetails.Detail = exception.Message;
            problemDetails.Status = StatusCodes.Status409Conflict;
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);

        return true;
    }
}
