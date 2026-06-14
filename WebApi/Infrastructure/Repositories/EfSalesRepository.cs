using WebApi.Infrastructure.Data;
using WebApi.Sales;

namespace WebApi.Infrastructure.Repositories;

public class EfSalesRepository(AppDbContext dbContext) : ISalesRepository
{
    public async Task<Sale> SaveSale(Sale newSale)
    {
        // Customer and Products referenced by the sale are already tracked within
        // the same scoped DbContext, so only the Sale graph is added.
        await dbContext.Sales.AddAsync(newSale);
        await dbContext.SaveChangesAsync();
        return newSale;
    }
}