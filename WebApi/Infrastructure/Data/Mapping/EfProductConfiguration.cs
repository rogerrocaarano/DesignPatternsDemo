using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Products;

namespace WebApi.Infrastructure.Data.Mapping;

public class EfProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(product => product.Id);

        builder.Property(product => product.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(product => product.UnitCost)
            .HasColumnType("decimal(18,2)");

        // Seed de productos de ejemplo (entra en la migración inicial).
        builder.HasData(
            new Product
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                Name = "Teclado mecánico",
                UnitCost = 250.00m
            },
            new Product
            {
                Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                Name = "Mouse inalámbrico",
                UnitCost = 120.00m
            },
            new Product
            {
                Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                Name = "Monitor 24 pulgadas",
                UnitCost = 980.00m
            },
            new Product
            {
                Id = Guid.Parse("44444444-4444-4444-4444-444444444444"),
                Name = "Audífonos con micrófono",
                UnitCost = 340.00m
            });
    }
}