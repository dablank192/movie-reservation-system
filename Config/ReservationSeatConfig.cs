using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;


namespace movie_reservation_system.Properties;

public class ReservationSeatConfig : IEntityTypeConfiguration<ReservationSeats>
{
    public void Configure (EntityTypeBuilder<ReservationSeats> builder)
    {
        builder.ToTable("ReservationSeats");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => new {t.ShowtimeId, t.SeatId})
        .IsUnique();

        builder.HasOne(t => t.Reservations)
        .WithMany(t => t.ReservationsSeats)
        .HasForeignKey(t => t.ReservationsId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Seats)
        .WithMany(t => t.ReservationSeats)
        .HasForeignKey(t => t.SeatId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Showtime)
        .WithMany(t => t.ReservationSeats)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
