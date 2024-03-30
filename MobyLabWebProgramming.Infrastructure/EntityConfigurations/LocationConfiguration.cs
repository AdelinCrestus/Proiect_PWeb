using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class LocationConfiguration : IEntityTypeConfiguration<Location>
{
    public void Configure(EntityTypeBuilder<Location> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .IsRequired();
        builder.Property(e => e.Address)
            .IsRequired();
        builder.Property(e => e.OpeningHour)
            .IsRequired();
        builder.Property(e => e.ClosingHour)
            .IsRequired();
        
    }
}
