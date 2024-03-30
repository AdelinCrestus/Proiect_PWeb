using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations;

public class TableConfiguration : IEntityTypeConfiguration<Table>
{
    public void Configure(EntityTypeBuilder<Table> builder)
    {
        builder.Property(e => e.Id).IsRequired();
        builder.HasKey(e => e.Id);
        builder.Property(e => e.Location)
            .IsRequired();
        /*builder.Property(e => e.Reservations)
            .IsRequired();*/
        builder.Property(e => e.Description)
            .HasMaxLength(1024)
            .IsRequired();
        builder.Property(e => e.CreatedAt)
            .IsRequired();
        builder.Property(e => e.UpdatedAt)
            .IsRequired();
        builder.Property(e => e.Quantity)
            .IsRequired();

        /*builder.HasOne(e => e.Location)
            .WithMany(e => e.Tables)
            .HasForeignKey(e => e.LocationId)
            .HasPrincipalKey(e => e.Id)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);*/
    }
}
