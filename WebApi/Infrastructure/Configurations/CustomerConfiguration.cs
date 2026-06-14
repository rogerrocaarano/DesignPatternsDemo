using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Customers;

namespace WebApi.Infrastructure.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers");

        builder.HasKey(customer => customer.Id);

        builder.Property(customer => customer.Nit)
            .IsRequired()
            .HasMaxLength(50);

        builder.HasIndex(customer => customer.Nit)
            .IsUnique();

        builder.Property(customer => customer.FullName)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(customer => customer.TotalSales)
            .HasColumnType("decimal(18,2)");
    }
}
