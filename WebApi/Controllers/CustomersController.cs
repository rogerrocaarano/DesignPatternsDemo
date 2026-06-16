using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Domain.Repositories;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/customers")]
[Tags("Customers")]
public class CustomersController : ControllerBase
{
    private readonly ICustomersRepository _repository;

    public CustomersController(ICustomersRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [EndpointName("ListCustomers")]
    [EndpointSummary("Lista todos los clientes, o busca uno por NIT mediante el query param 'nit'.")]
    public async Task<IActionResult> ListCustomers([FromQuery] string? nit)
    {
        if (string.IsNullOrWhiteSpace(nit))
        {
            var customers = await _repository.ListAllCustomersAsync();
            return Ok(customers.Select(CustomerDto.FromEntity));
        }

        var customer = await _repository.SearchCustomerByNitAsync(nit);
        return customer is null
            ? NotFound()
            : Ok(CustomerDto.FromEntity(customer));
    }

    [HttpGet("{id:guid}")]
    [EndpointName("GetCustomer")]
    [EndpointSummary("Obtiene el detalle de un cliente.")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customer = await _repository.SearchCustomerByIdAsync(id);
        return customer is null
            ? NotFound()
            : Ok(CustomerDto.FromEntity(customer));
    }
}
