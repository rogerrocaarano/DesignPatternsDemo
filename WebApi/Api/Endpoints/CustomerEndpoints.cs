using WebApi.Api.DTOs;
using WebApi.Customers;

namespace WebApi.Api.Endpoints;

public static class CustomerEndpoints
{
    public static IEndpointRouteBuilder MapCustomerEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/customers").WithTags("Customers");

        group.MapGet("/", async (ICustomersRepository repository) =>
            {
                var customers = await repository.ListAllCustomersAsync();
                return Results.Ok(customers.Select(CustomerDto.FromEntity));
            })
            .WithName("ListCustomers")
            .WithSummary("Lista todos los clientes.");

        group.MapGet("/ids", async (ICustomersRepository repository) =>
                Results.Ok(await repository.ListAllCustomerIdsAsync()))
            .WithName("ListCustomerIds")
            .WithSummary("Lista los IDs de los clientes.");

        group.MapGet("/by-nit/{nit}", async (string nit, ICustomersRepository repository) =>
            {
                var customer = await repository.SearchCustomerByNitAsync(nit);
                return customer is null
                    ? Results.NotFound()
                    : Results.Ok(CustomerDto.FromEntity(customer));
            })
            .WithName("GetCustomerByNit")
            .WithSummary("Busca un cliente por su NIT (404 si no existe).");

        group.MapGet("/{id:guid}", async (Guid id, ICustomersRepository repository) =>
            {
                var customer = await repository.SearchCustomerByIdAsync(id);
                return customer is null
                    ? Results.NotFound()
                    : Results.Ok(CustomerDto.FromEntity(customer));
            })
            .WithName("GetCustomer")
            .WithSummary("Obtiene el detalle de un cliente.");

        return app;
    }
}