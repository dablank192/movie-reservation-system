using System;
using Microsoft.EntityFrameworkCore;
using movie_reservation_system.Model;


namespace movie_reservation_system.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext (DbContextOptions<AppDbContext> options) : base (options) {}

    public DbSet<User> User {get; set;}
    public DbSet<Movies> Movies {get; set;}
    public DbSet<Reservations> Reservations {get; set;}
    public DbSet<ReservationSeats> ReservationSeats {get; set;}
    public DbSet<Rooms> Rooms {get; set;}
    public DbSet<Seats> Seats {get; set;}
    public DbSet<Showtime> Showtime {get; set;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
