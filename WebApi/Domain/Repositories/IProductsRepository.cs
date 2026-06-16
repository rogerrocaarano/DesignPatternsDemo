using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface IProductsRepository
{
    Task<IReadOnlyList<Product>> ListAllProductsAsync();

    Task<Product?> SearchProductByIdAsync(Guid id);
}