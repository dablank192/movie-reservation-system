using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;


namespace movie_reservation_system.Config;

public class ReservationsConfig : IEntityTypeConfiguration<Reservations>
{
    public void Configure(EntityTypeBuilder<Reservations> builder)
    {
        builder.ToTable("Reservations");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.TotalAmount);

        builder.HasIndex(t => t.Status);


        builder.HasOne(t => t.User)
        .WithMany(t => t.Reservations)
        .HasForeignKey(t => t.UserId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Showtime)
        .WithMany(t => t.Reservations)
        .HasForeignKey(t => t.ShowtimeId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
