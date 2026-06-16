using Microsoft.EntityFrameworkCore;
using WebApi.Customers.UseCases;
using WebApi.Domain.Repositories;
using WebApi.Infrastructure.Data;
using WebApi.Infrastructure.Repositories;
using WebApi.Products.UseCases;
using WebApi.Sales.Handlers;
using WebApi.Sales.Strategies.Discounts;
using WebApi.Sales.UseCases;

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
        services.AddProductUseCases();
        services.AddCustomerUseCases();

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
        services.AddScoped<DiscountStrategyContext>();
    }

    private static void AddSalesPipeline(this IServiceCollection services)
    {
        services.AddTransient<SearchOrCreateCustomerHandler>();
        services.AddTransient<CreateSaleHandler>();
        services.AddTransient<ApplyDiscountHandler>();
        services.AddTransient<PersistSaleHandler>();
        services.AddScoped<SaleCreationUseCase>();
        services.AddScoped<CalculateDiscountUseCase>();
    }

    private static void AddProductUseCases(this IServiceCollection services)
    {
        services.AddScoped<ListProductsUseCase>();
        services.AddScoped<GetProductUseCase>();
    }

    private static void AddCustomerUseCases(this IServiceCollection services)
    {
        services.AddScoped<ListCustomersUseCase>();
        services.AddScoped<GetCustomerUseCase>();
        services.AddScoped<SearchCustomerByNitUseCase>();
    }
}