using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Sales;

namespace WebApi.Infrastructure.Configurations;

public class SaleConfiguration : IEntityTypeConfiguration<Sale>
{
    public void Configure(EntityTypeBuilder<Sale> builder)
    {
        builder.ToTable("Sales");

        builder.HasKey(sale => sale.Id);

        builder.HasOne(sale => sale.Customer)
            .WithMany()
            .HasForeignKey("CustomerId")
            .OnDelete(DeleteBehavior.Restrict)
            .IsRequired();

        // La colección se respalda en el campo privado _items del dominio.
        builder.HasMany(sale => sale.Items)
            .WithOne()
            .OnDelete(DeleteBehavior.Cascade);

        builder.Navigation(sale => sale.Items)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}
