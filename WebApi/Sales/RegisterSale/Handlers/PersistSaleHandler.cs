using WebApi.Customers;

namespace WebApi.Sales.RegisterSale.Handlers;

public class PersistSaleHandler(SalesRepository salesRepository, CustomersRepository customersRepository)
    : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var sale = context.SaleEntity;
        var customer = context.CustomerEntity;

        var saleAmount = sale.CalculateTotal();
        var discount = context.SaleDiscount;
        if (discount != null)
        {
            saleAmount = saleAmount - discount.Amount;
        }
        
        customer.IncreaseTotalSales(saleAmount);
        await customersRepository.UpsertCustomerAsync(customer);
        await salesRepository.SaveSale(sale);

        await base.HandleAsync(context);
    }
}