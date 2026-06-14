namespace WebApi.Sales;

public interface ISalesRepository
{
    Task<Sale> SaveSale(Sale newSale);
}