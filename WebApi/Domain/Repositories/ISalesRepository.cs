using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface ISalesRepository
{
    Task<Sale> SaveSaleAsync(Sale newSale);
}