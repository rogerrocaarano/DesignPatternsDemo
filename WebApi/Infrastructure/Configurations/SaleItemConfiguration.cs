using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Sales;

namespace WebApi.Infrastructure.Configurations;

public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
{
    public void Configure(EntityTypeBuilder<SaleItem> builder)
    {
        builder.ToTable("SaleItems");

        // SaleItem no tiene clave en el dominio: se usa una clave shadow.
        builder.Property<int>("Id");
        builder.HasKey("Id");

        builder.Property(item => item.Quantity)
            .IsRequired();

        // El producto referenciado no se debe insertar/eliminar desde la venta.
        builder.HasOne(item => item.Item)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();
    }
}
