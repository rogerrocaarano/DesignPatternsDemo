namespace WebApi.Customers;

public class CustomersRepository
{
    public async Task<Customer?> SearchCustomerByNitAsync(string nit)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Customer?> SearchCustomerByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }
    
    public async Task<Customer> UpsertCustomerAsync(Customer customer)
    {
        throw new NotImplementedException();
    }
}