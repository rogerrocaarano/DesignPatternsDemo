namespace WebApi.Products;

public interface IProductsRepository
{
    Task<IReadOnlyList<Product>> ListAllProductsAsync();

    Task<IReadOnlyList<Guid>> ListAllProductIdsAsync();

    Task<Product?> SearchProductByIdAsync(Guid id);
}
