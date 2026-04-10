using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace movie_reservation_system.Config;

public class UserConfig : IEntityTypeConfiguration<Model.User>
{
    public void Configure(EntityTypeBuilder<Model.User> builder)
    {
        builder.ToTable("User");
        builder.HasKey(t => t.Id);

        builder.HasIndex(t => t.Id);

        builder.HasIndex(t => t.Username);

        builder.Property(t => t.HashPassword).IsRequired();

        builder.Property(t => t.Roles).IsRequired();
    }
}
