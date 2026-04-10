using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using movie_reservation_system.Model;


namespace movie_reservation_system.Config;

public class MoviesConfig : IEntityTypeConfiguration<Movies>
{
    public void Configure(EntityTypeBuilder<Movies> builder)
    {
        builder.ToTable("Movies");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Title);
        builder.Property(t => t.Title).IsRequired();

        builder.HasIndex(t => t.Description);
    }

}
