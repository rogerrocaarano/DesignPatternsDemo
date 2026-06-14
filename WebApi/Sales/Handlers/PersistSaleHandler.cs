using WebApi.Customers;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class PersistSaleHandler(ISalesRepository salesRepository, ICustomersRepository customersRepository)
    : SaleBaseHandler
{
    public override async Task HandleAsync(SaleContext context)
    {
        var sale = context.SaleEntity!;
        var customer = context.CustomerEntity!;

        var saleAmount = sale.CalculateTotal();
        var discount = context.SaleDiscount;
        if (discount != null) saleAmount -= discount.Amount;

        customer.IncreaseTotalSales(saleAmount);
        await customersRepository.UpsertCustomerAsync(customer);
        await salesRepository.SaveSale(sale);

        await base.HandleAsync(context);
    }
}