using WebApi.Customers.Actions;
using WebApi.Customers.Results;
using WebApi.Domain.Repositories;

namespace WebApi.Customers.UseCases;

public class SearchCustomerByNitUseCase
{
    private readonly ICustomersRepository _repository;

    public SearchCustomerByNitUseCase(ICustomersRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerResult?> RealizeAsync(SearchCustomerByNitAction action)
    {
        var customer = await _repository.SearchCustomerByNitAsync(action.Nit);
        return customer is null ? null : CustomerResult.FromEntity(customer);
    }
}
