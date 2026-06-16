using WebApi.Domain.Entities;

namespace WebApi.Controllers.DTOs;

public record CustomerDto(Guid Id, string Nit, string FullName, decimal TotalSales)
{
    public static CustomerDto FromEntity(Customer customer)
    {
        return new CustomerDto(customer.Id, customer.Nit, customer.FullName, customer.TotalSales);
    }
}