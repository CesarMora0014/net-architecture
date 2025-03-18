using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Pacagroup.Ecommerce.Domain.Entities;


namespace Pacagroup.Ecommerce.Persistence.Configurations;

public class DiscountConfiguration : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.Property(table => table.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(table => table.Description)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(table => table.Percent)
            .HasPrecision(9,3)
            .IsRequired();


    }
}
