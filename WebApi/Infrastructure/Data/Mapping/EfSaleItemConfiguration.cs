using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.ValueObjects;
using WebApi.Sales;

namespace WebApi.Infrastructure.Data.Mapping;

public class EfSaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
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