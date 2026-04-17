using System;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Dto;
using movie_reservation_system.Model;


namespace movie_reservation_system.Infrastructure;

public static class DataSeeder
{
    public static async Task ExecuteAsync (IServiceProvider serviceProvider) //Use to access to DI container
    {
        using var context = new AppDbContext(
            serviceProvider.GetService<DbContextOptions<AppDbContext>>()
        );

        if (await context.Rooms.AnyAsync())
        {
            return;
        }

        var room1 = new Rooms
        {
            Name= "Cinema 1",
            TotalCapacity= 50
        };
        var room2 = new Rooms
        {
            Name= "Cinema 2",
            TotalCapacity= 50
        };

        await context.Rooms.AddRangeAsync(room1, room2);

        await context.SaveChangesAsync();



        var seats = new List<Seats>();

        string[] rows = ["A", "B", "C", "D", "E"];

        foreach (var room in new[] {room1, room2})
        {
            foreach (var row in rows)
            {
                for (int number = 1; number <= 10; number++)
                {
                    seats.Add( new Seats
                    {
                        RoomId= room.Id,
                        Row= row,
                        Number= number,
                        Type= (row == "D" || row == "E") ? SeatsType.Vip : SeatsType.Normal
                    });
                }
            }
        }
        await context.Seats.AddRangeAsync(seats);

        var movies = new List<Movies>
        {
            new()
            {
                Title= "Avenger 3",
                Description= "the avenger sdaifoa",
                Duration= TimeSpan.FromMinutes(150),
                Category= MoviesCategory.Action,
                Status= MovieStatus.NowShowing,
            },

            new()
            {
                Title= "Annaconda",
                Description= "about python",
                Duration= TimeSpan.FromMinutes(120),
                Category= MoviesCategory.Action,
                Status= MovieStatus.NowShowing
            },

            new()
            {
                Title= "Mai",
                Description= "story about mai",
                Duration= TimeSpan.FromMinutes(160),
                Category= MoviesCategory.Comedy,
                Status= MovieStatus.CommingSoon
            },

            new()
            {
                Title= "The Nun",
                Description= "story about mai",
                Duration= TimeSpan.FromMinutes(160),
                Category= MoviesCategory.Horror,
                Status= MovieStatus.Stopped
            }
        };

        await context.Movies.AddRangeAsync(movies);
        await context.SaveChangesAsync();

        var today = DateTime.UtcNow;

        var showtime = new List<Showtime>
        {
            new()
            {
                MovieId= movies[1].Id,
                RoomId= room1.Id,
                StartTime= today.AddHours(18),
                EndTime= today.AddHours(18).AddMinutes(movies[1].Duration?.TotalMinutes ?? 0)
            },

            new()
            {
                MovieId= movies[2].Id,
                RoomId= room1.Id,
                StartTime= today.AddHours(21),
                EndTime= today.AddHours(21).AddMinutes(movies[1].Duration?.TotalMinutes ?? 0)
            },

            new()
            {
                MovieId= movies[3].Id,
                RoomId= room1.Id,
                StartTime= today.AddHours(15),
                EndTime= today.AddHours(15).AddMinutes(movies[1].Duration?.TotalMinutes ?? 0)
            },

            new()
            {
                MovieId= movies[4].Id,
                RoomId= room1.Id,
                StartTime= today.AddDays(1).AddHours(18),
                EndTime= today.AddHours(18).AddMinutes(movies[1].Duration?.TotalMinutes ?? 0)
            }
        };

        await context.AddRangeAsync(showtime);
        await context.SaveChangesAsync();
    }
}
