using System;
using Ardalis.Specification.EntityFrameworkCore;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Exception.Showtime;
using movie_reservation_system.Features.Showtime.Specifications;
using movie_reservation_system.Infrastructure;


namespace movie_reservation_system.Features.Showtime.UpdateShowtime;

public class UpdateShowtime : Endpoint<RequestModel, ResponseModel>
{
    public AppDbContext _context {get; set;}

    public override void Configure()
    {
        Patch("/{id}");
        Roles("Admin");
        Group<ShowtimeApi>();
    }

    public override async Task HandleAsync(RequestModel req, CancellationToken ct)
    {
        int id = req.Id;

        var showtime = await _context.Showtime.FirstOrDefaultAsync(t => t.Id == id, ct) ?? throw new ShowtimeNotFoundException(id);

        var movie = await _context.Movies.FindAsync(req.MovieId, ct);

        DateTime showStartTime = req.StartTime ?? showtime.StartTime;

        DateTime showEndTime = showStartTime.Add(movie.Duration ?? TimeSpan.MinValue).AddMinutes(15);

        var checkOverlap = new ShowtimeOverloadSpec (
            roomId: req.RoomId ?? showtime.RoomId,
            movieStartTime: showStartTime,
            movieEndTime: showEndTime,
            excludeShowtimeId: req.Id
        );

        var showOverlap = await _context.Showtime.WithSpecification(checkOverlap).AnyAsync(ct);

        if (showOverlap)
        {
            throw new ShowtimeOverlappedException(showtime.MovieId);
        }

        showtime.MovieId = req.MovieId ?? showtime.MovieId;
        showtime.RoomId = req.RoomId ?? showtime.RoomId;
        showtime.StartTime = showStartTime;
        showtime.EndTime = showEndTime;

        _context.Showtime.Update(showtime);

        await _context.SaveChangesAsync(ct);

        var response = new ResponseModel
        {
            ShowtimeId= showtime.Id,
            MovieId= showtime.MovieId,
            StartTime= showtime.StartTime,
            EndTime= showtime.EndTime
        };

        await Send.OkAsync(response, ct);
    }
}
