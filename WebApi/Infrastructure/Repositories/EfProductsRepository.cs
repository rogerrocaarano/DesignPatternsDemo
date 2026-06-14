using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure.Data;
using WebApi.Products;

namespace WebApi.Infrastructure.Repositories;

public class EfProductsRepository(AppDbContext dbContext) : IProductsRepository
{
    public async Task<IReadOnlyList<Product>> ListAllProductsAsync()
    {
        return await dbContext.Products
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Guid>> ListAllProductIdsAsync()
    {
        return await dbContext.Products
            .AsNoTracking()
            .Select(product => product.Id)
            .ToListAsync();
    }

    public async Task<Product?> SearchProductByIdAsync(Guid id)
    {
        // Tracked on purpose: when a product is reused while building a sale,
        // EF must treat it as an existing row instead of inserting a duplicate.
        return await dbContext.Products.FindAsync(id);
    }
}