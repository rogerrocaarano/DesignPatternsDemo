using WebApi.Domain.Entities;
using WebApi.Domain.Repositories;

namespace WebApi.Sales.Handlers;

public class SearchOrCreateCustomerHandler : SaleBaseHandler
{
    private readonly ICustomersRepository _customersRepository;

    public SearchOrCreateCustomerHandler(ICustomersRepository customersRepository)
    {
        _customersRepository = customersRepository;
    }


    public override async Task HandleAsync(SaleHandlerContext handlerContext)
    {
        var existingCustomer = await _customersRepository.SearchCustomerByNitAsync(handlerContext.CustomerNit);

        handlerContext.IsNewCustomer = existingCustomer is null;
        handlerContext.CustomerEntity = existingCustomer ??
                                 new Customer(handlerContext.CustomerNit, handlerContext.CustomerFullName);

        await base.HandleAsync(handlerContext);
    }
}