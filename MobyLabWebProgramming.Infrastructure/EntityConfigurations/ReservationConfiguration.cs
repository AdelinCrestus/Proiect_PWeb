using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Start).IsRequired();
        builder.Property(e => e.End).IsRequired();
        builder.Property(e => e.optionalDetails)
            .HasMaxLength(255)
            .IsRequired();

        builder.HasOne(e => e.Table)
            .WithMany(e => e.Reservations)
            .HasForeignKey(e => e.TableId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);

    }
}
