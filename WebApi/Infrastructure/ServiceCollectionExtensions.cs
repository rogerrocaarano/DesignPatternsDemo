using Microsoft.EntityFrameworkCore;
using WebApi.Customers;
using WebApi.Discounts;
using WebApi.Discounts.Strategies;
using WebApi.Products;
using WebApi.Sales;
using WebApi.Sales.RegisterSale;
using WebApi.Sales.RegisterSale.Handlers;

namespace WebApi.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=app.db";
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));

        // Repositories
        services.AddScoped<ICustomersRepository, CustomersRepository>();
        services.AddScoped<IProductsRepository, ProductsRepository>();
        services.AddScoped<ISalesRepository, SalesRepository>();

        // Discounts (Strategy pattern)
        services.AddScoped<IDiscountStrategy, NewCustomerDiscount>();
        services.AddScoped<DiscountService>();

        // Register-sale pipeline (Chain of Responsibility)
        services.AddScoped<SearchOrCreateCustomerHandler>();
        services.AddScoped<CreateSaleHandler>();
        services.AddScoped<ApplyDiscountHandler>();
        services.AddScoped<PersistSaleHandler>();
        services.AddScoped<RegisterSalePipeline>();

        return services;
    }
}
