using WebApi.Customers;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class SearchOrCreateCustomerHandler(ICustomersRepository repository) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(SaleContext context)
    {
        var request = context.SaleRequest;
        var existingCustomer = await repository.SearchCustomerByNitAsync(request.CustomerNit);

        context.IsNewCustomer = existingCustomer is null;
        context.CustomerEntity = existingCustomer ??
                                 new Customer(request.CustomerNit, request.CustomerFullName);

        await base.HandleAsync(context);
    }
}