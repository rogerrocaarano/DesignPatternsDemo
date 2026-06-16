using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Products.Actions;
using WebApi.Products.UseCases;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/products")]
[Tags("Products")]
public class ProductsController : ControllerBase
{
    private readonly ListProductsUseCase _listProductsUseCase;
    private readonly GetProductUseCase _getProductUseCase;

    public ProductsController(ListProductsUseCase listProductsUseCase, GetProductUseCase getProductUseCase)
    {
        _listProductsUseCase = listProductsUseCase;
        _getProductUseCase = getProductUseCase;
    }

    [HttpGet]
    [EndpointName("ListProducts")]
    [EndpointSummary("Lista todos los productos.")]
    public async Task<IActionResult> ListProducts()
    {
        var products = await _listProductsUseCase.RealizeAsync();
        return Ok(products);
    }

    [HttpGet("{id:guid}")]
    [EndpointName("GetProduct")]
    [EndpointSummary("Obtiene el detalle de un producto.")]
    public async Task<IActionResult> GetProduct(Guid id)
    {
        var product = await _getProductUseCase.RealizeAsync(new GetProductAction(id));
        return product is null
            ? NotFound()
            : Ok(product);
    }
}
