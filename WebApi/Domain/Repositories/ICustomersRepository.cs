using WebApi.Domain.Entities;

namespace WebApi.Domain.Repositories;

public interface ICustomersRepository
{
    Task<IReadOnlyList<Customer>> ListAllCustomersAsync();

    Task<Customer?> SearchCustomerByNitAsync(string nit);

    Task<Customer?> SearchCustomerByIdAsync(Guid id);

    Task<Customer> UpsertCustomerAsync(Customer customer);
}