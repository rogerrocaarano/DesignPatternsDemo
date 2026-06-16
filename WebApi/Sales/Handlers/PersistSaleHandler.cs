using WebApi.Domain.Repositories;

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

    public override async Task HandleAsync(SaleHandlerContext handlerContext)
    {
        var sale = handlerContext.SaleEntity!;
        var customer = handlerContext.CustomerEntity!;

        var saleAmount = sale.CalculateTotal();
        var discount = handlerContext.SaleDiscount;
        if (discount != null) saleAmount -= discount.Amount;

        customer.IncreaseTotalSales(saleAmount);
        await _customersRepository.UpsertCustomerAsync(customer);
        await _salesRepository.SaveSaleAsync(sale);

        await base.HandleAsync(handlerContext);
    }
}