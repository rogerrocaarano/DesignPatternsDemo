using Microsoft.EntityFrameworkCore;
using WebApi.Infrastructure;

namespace WebApi.Customers;

public class CustomersRepository(AppDbContext dbContext) : ICustomersRepository
{
    public async Task<IReadOnlyList<Customer>> ListAllCustomersAsync()
    {
        return await dbContext.Customers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IReadOnlyList<Guid>> ListAllCustomerIdsAsync()
    {
        return await dbContext.Customers
            .AsNoTracking()
            .Select(customer => customer.Id)
            .ToListAsync();
    }

    public async Task<Customer?> SearchCustomerByNitAsync(string nit)
    {
        return await dbContext.Customers
            .FirstOrDefaultAsync(customer => customer.Nit == nit);
    }

    public async Task<Customer?> SearchCustomerByIdAsync(Guid id)
    {
        return await dbContext.Customers.FindAsync(id);
    }

    public async Task<Customer> UpsertCustomerAsync(Customer customer)
    {
        var exists = await dbContext.Customers
            .AsNoTracking()
            .AnyAsync(existing => existing.Id == customer.Id);

        if (exists)
        {
            dbContext.Customers.Update(customer);
        }
        else
        {
            await dbContext.Customers.AddAsync(customer);
        }

        await dbContext.SaveChangesAsync();
        return customer;
    }
}
