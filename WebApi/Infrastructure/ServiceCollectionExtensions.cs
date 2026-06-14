using Microsoft.EntityFrameworkCore;
using WebApi.Customers;
using WebApi.Discounts;
using WebApi.Discounts.Strategies;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories;
using WebApi.Products;
using WebApi.Sales;
using WebApi.Sales.Handlers;
using WebApi.Sales.Pipelines;

namespace WebApi.Infrastructure;

public static class ServiceCollectionExtensions
{
    public const string DevelopmentCorsPolicy = "DevelopmentCors";

    public static IServiceCollection AddApplicationServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddDevelopmentCors();
        services.AddRepositories();
        services.AddDiscounts();
        services.AddSalesPipeline();

        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Default") ?? "Data Source=app.db";
        services.AddDbContext<AppDbContext>(options => options.UseSqlite(connectionString));
    }

    private static void AddDevelopmentCors(this IServiceCollection services)
    {
        services.AddCors(options => options.AddPolicy(DevelopmentCorsPolicy, policy =>
            policy.WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod()));
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<ICustomersRepository, EfCustomersRepository>();
        services.AddScoped<IProductsRepository, EfProductsRepository>();
        services.AddScoped<ISalesRepository, EfSalesRepository>();
    }

    private static void AddDiscounts(this IServiceCollection services)
    {
        services.AddScoped<IDiscountStrategy, NewCustomerDiscount>();
        services.AddScoped<IDiscountStrategy, SaleAmountDiscount>();
        services.AddScoped<IDiscountStrategy, VipClientDiscount>();
        services.AddScoped<DiscountService>();
    }

    private static void AddSalesPipeline(this IServiceCollection services)
    {
        services.AddTransient<SearchOrCreateCustomerHandler>();
        services.AddTransient<CreateSaleHandler>();
        services.AddTransient<ApplyDiscountHandler>();
        services.AddTransient<PersistSaleHandler>();
        services.AddScoped<SalePipeline>();
        services.AddScoped<CalculateDiscountPipeline>();
    }
}