using WebApi.Customers.Actions;
using WebApi.Customers.Results;
using WebApi.Domain.Repositories;

namespace WebApi.Customers.UseCases;

public class GetCustomerUseCase
{
    private readonly ICustomersRepository _repository;

    public GetCustomerUseCase(ICustomersRepository repository)
    {
        _repository = repository;
    }

    public async Task<CustomerResult?> RealizeAsync(GetCustomerAction action)
    {
        var customer = await _repository.SearchCustomerByIdAsync(action.Id);
        return customer is null ? null : CustomerResult.FromEntity(customer);
    }
}
