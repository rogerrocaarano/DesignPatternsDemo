using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Customers.Actions;
using WebApi.Customers.UseCases;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/customers")]
[Tags("Customers")]
public class CustomersController : ControllerBase
{
    private readonly ListCustomersUseCase _listCustomersUseCase;
    private readonly GetCustomerUseCase _getCustomerUseCase;
    private readonly SearchCustomerByNitUseCase _searchCustomerByNitUseCase;

    public CustomersController(
        ListCustomersUseCase listCustomersUseCase,
        GetCustomerUseCase getCustomerUseCase,
        SearchCustomerByNitUseCase searchCustomerByNitUseCase)
    {
        _listCustomersUseCase = listCustomersUseCase;
        _getCustomerUseCase = getCustomerUseCase;
        _searchCustomerByNitUseCase = searchCustomerByNitUseCase;
    }

    [HttpGet]
    [EndpointName("ListCustomers")]
    [EndpointSummary("Lista todos los clientes, o busca uno por NIT mediante el query param 'nit'.")]
    public async Task<IActionResult> ListCustomers([FromQuery] string? nit)
    {
        if (string.IsNullOrWhiteSpace(nit))
        {
            var customers = await _listCustomersUseCase.RealizeAsync();
            return Ok(customers);
        }

        var customer = await _searchCustomerByNitUseCase.RealizeAsync(new SearchCustomerByNitAction(nit));
        return customer is null
            ? NotFound()
            : Ok(customer);
    }

    [HttpGet("{id:guid}")]
    [EndpointName("GetCustomer")]
    [EndpointSummary("Obtiene el detalle de un cliente.")]
    public async Task<IActionResult> GetCustomer(Guid id)
    {
        var customer = await _getCustomerUseCase.RealizeAsync(new GetCustomerAction(id));
        return customer is null
            ? NotFound()
            : Ok(customer);
    }
}
