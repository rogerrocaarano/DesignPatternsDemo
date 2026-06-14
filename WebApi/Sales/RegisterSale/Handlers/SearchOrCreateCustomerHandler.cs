using WebApi.Customers;

namespace WebApi.Sales.RegisterSale.Handlers;

public class SearchOrCreateCustomerHandler(ICustomersRepository repository) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var request = context.RegisterSaleRequest;
        var existingCustomer = await repository.SearchCustomerByNitAsync(request.CustomerNit);

        context.IsNewCustomer = existingCustomer is null;
        context.CustomerEntity = existingCustomer ??
                                 new Customer(request.CustomerNit, request.CustomerFullName);

        await base.HandleAsync(context);
    }
}
