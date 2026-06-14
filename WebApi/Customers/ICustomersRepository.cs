namespace WebApi.Customers;

public interface ICustomersRepository
{
    Task<IReadOnlyList<Customer>> ListAllCustomersAsync();

    Task<IReadOnlyList<Guid>> ListAllCustomerIdsAsync();

    Task<Customer?> SearchCustomerByNitAsync(string nit);

    Task<Customer?> SearchCustomerByIdAsync(Guid id);

    Task<Customer> UpsertCustomerAsync(Customer customer);
}