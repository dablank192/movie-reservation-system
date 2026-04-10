using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;


namespace movie_reservation_system.Config;

public class ShowtimeConfig : IEntityTypeConfiguration<Showtime>
{
    public void Configure (EntityTypeBuilder<Showtime> builder)
    {
        builder.ToTable("Showtime");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.StartTime).IsRequired();

        builder.Property(t => t.EndTime).IsRequired();

        builder.HasOne(t => t.Movies)
        .WithMany(t => t.Showtime)
        .HasForeignKey(t => t.MovieId)
        .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Rooms)
        .WithMany(t => t.Showtime)
        .HasForeignKey(t => t.RoomId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
