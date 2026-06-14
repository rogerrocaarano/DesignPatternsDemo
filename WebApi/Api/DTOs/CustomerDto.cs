using WebApi.Customers;

namespace WebApi.Api.DTOs;

public record CustomerDto(Guid Id, string Nit, string FullName, decimal TotalSales)
{
    public static CustomerDto FromEntity(Customer customer)
    {
        return new CustomerDto(customer.Id, customer.Nit, customer.FullName, customer.TotalSales);
    }
}