using System;
using Ardalis.Specification.EntityFrameworkCore;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Features.Showtime.Specifications;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Showtime.AddShowtime;

public class AddShowtime : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Post("/");
        Group<ShowtimeApi>();
    }

    public override async Task HandleAsync (RequestModel req, CancellationToken ct)
    {
        var movie = await _context.Movies.FindAsync(req.MovieId, ct);
        
        DateTime startTime = req.StartTime;

        DateTime endTime = startTime.Add(movie.Duration ?? TimeSpan.Zero).AddMinutes(15);

        var overlap = new ShowtimeOverloadSpec(
            roomId: req.RoomId,
            movieStartTime: startTime,
            movieEndTime: endTime
        );

        var checkOverlap = await _context.Showtime.WithSpecification(overlap).AnyAsync(ct); //check to see if any movies showtime get overlap 

        if (checkOverlap)
        {
            throw new ShowtimeOverlappedException(req.MovieId);
        }

        var newShowtime = new Model.Showtime
        {
            MovieId= req.MovieId,
            RoomId= req.RoomId,
            StartTime= startTime,
            EndTime= endTime
        };

        await _context.Showtime.AddAsync(newShowtime, ct);

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            ShowtimeId= newShowtime.Id,
            MovieId= newShowtime.MovieId,
            StartTime= newShowtime.StartTime,
            EndTime= newShowtime.EndTime
        };

        await Send.OkAsync(response, ct);
    }
}
