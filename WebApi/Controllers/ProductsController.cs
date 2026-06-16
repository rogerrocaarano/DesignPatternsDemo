using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Domain.Repositories;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/products")]
[Tags("Products")]
public class ProductsController : ControllerBase
{
    private readonly IProductsRepository _repository;

    public ProductsController(IProductsRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [EndpointName("ListProducts")]
    [EndpointSummary("Lista todos los productos.")]
    public async Task<IActionResult> ListProducts()
    {
        var products = await _repository.ListAllProductsAsync();
        return Ok(products.Select(ProductDto.FromEntity));
    }

    [HttpGet("{id:guid}")]
    [EndpointName("GetProduct")]
    [EndpointSummary("Obtiene el detalle de un producto.")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _repository.SearchProductByIdAsync(id);
        return product is null
            ? NotFound()
            : Ok(ProductDto.FromEntity(product));
    }
}
