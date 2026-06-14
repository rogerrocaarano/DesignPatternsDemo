using WebApi.Customers;

namespace WebApi.Sales.RegisterSale.Handlers;

public class SearchOrCreateCustomerHandler(CustomersRepository repository) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var sale = context.RegisterSaleRequest;
        var customer = await repository.SearchCustomerByNitAsync(sale.CustomerNit) ?? 
                       new Customer(sale.CustomerNit, sale.CustomerFullName);

        context.CustomerEntity = customer;
        await base.HandleAsync(context);
    }
}