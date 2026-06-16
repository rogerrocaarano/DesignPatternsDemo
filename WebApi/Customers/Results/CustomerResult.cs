using WebApi.Domain.Entities;

namespace WebApi.Customers.Results;

public record CustomerResult(Guid Id, string Nit, string FullName, decimal TotalSales)
{
    public static CustomerResult FromEntity(Customer customer)
    {
        return new CustomerResult(customer.Id, customer.Nit, customer.FullName, customer.TotalSales);
    }
}
