using WebApi.Customers.Results;
using WebApi.Domain.Repositories;

namespace WebApi.Customers.UseCases;

public class ListCustomersUseCase
{
    private readonly ICustomersRepository _repository;

    public ListCustomersUseCase(ICustomersRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<CustomerResult>> RealizeAsync()
    {
        var customers = await _repository.ListAllCustomersAsync();
        return customers.Select(CustomerResult.FromEntity).ToList();
    }
}
