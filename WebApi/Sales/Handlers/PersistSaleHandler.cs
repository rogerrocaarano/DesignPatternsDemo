using WebApi.Customers;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class PersistSaleHandler
    : SaleBaseHandler
{
    private readonly ICustomersRepository _customersRepository;
    private readonly ISalesRepository _salesRepository;

    public PersistSaleHandler(ISalesRepository salesRepository, ICustomersRepository customersRepository)
    {
        _salesRepository = salesRepository;
        _customersRepository = customersRepository;
    }

    public override async Task HandleAsync(SaleContext context)
    {
        var sale = context.SaleEntity!;
        var customer = context.CustomerEntity!;

        var saleAmount = sale.CalculateTotal();
        var discount = context.SaleDiscount;
        if (discount != null) saleAmount -= discount.Amount;

        customer.IncreaseTotalSales(saleAmount);
        await _customersRepository.UpsertCustomerAsync(customer);
        await _salesRepository.SaveSale(sale);

        await base.HandleAsync(context);
    }
}