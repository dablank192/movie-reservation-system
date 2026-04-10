using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;


namespace movie_reservation_system.Config;

public class RoomsConfig : IEntityTypeConfiguration<Rooms>
{
    public void Configure (EntityTypeBuilder<Rooms> builder)
    {
        builder.ToTable("Rooms");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Name);
    }
}
