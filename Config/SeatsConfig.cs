using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;



namespace movie_reservation_system.Config;

public class SeatsConfig : IEntityTypeConfiguration<Seats>
{
    public void Configure (EntityTypeBuilder<Seats> builder)
    {
        builder.ToTable("Seats");
        builder.HasKey(t => t.Id);

        builder.HasOne(t => t.Rooms)
        .WithMany(t => t.Seats)
        .HasForeignKey(t => t.RoomId)
        .OnDelete(DeleteBehavior.Restrict);
    }
}
