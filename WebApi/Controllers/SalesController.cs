using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using WebApi.Controllers.DTOs;
using WebApi.Controllers.Filters;
using WebApi.Sales.Actions;
using WebApi.Sales.UseCases;

namespace WebApi.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/sales")]
[Tags("Sales")]
[ValidateSaleRequest]
public class SalesController : ControllerBase
{
    private readonly SaleCreationUseCase _creationUseCase;
    private readonly CalculateDiscountUseCase _calculateDiscountUseCase;

    public SalesController(SaleCreationUseCase creationUseCase, CalculateDiscountUseCase calculateDiscountUseCase)
    {
        _creationUseCase = creationUseCase;
        _calculateDiscountUseCase = calculateDiscountUseCase;
    }

    [HttpPost("discount")]
    [EndpointName("CalculateSaleDiscount")]
    [EndpointSummary("Calcula el descuento de una venta simulada (no la registra).")]
    public async Task<IActionResult> CalculateSaleDiscount(CreateSaleRequest request)
    {
        var action = new CalculateDiscountAction(request.CustomerFullName, request.CustomerNit, MapItems(request));
        var result = await _calculateDiscountUseCase.RealizeAsync(action);
        return Ok(result);
    }

    [HttpPost]
    [EndpointName("RegisterSale")]
    [EndpointSummary("Registra una venta completa, incluyendo los descuentos aplicados.")]
    public async Task<IActionResult> RegisterSale(CreateSaleRequest request)
    {
        var action = new SaleCreationAction(request.CustomerFullName, request.CustomerNit, MapItems(request));
        var result = await _creationUseCase.RealizeAsync(action);
        return Created($"/v1/sales/{result.SaleId}", result);
    }

    private static List<SaleItemValue> MapItems(CreateSaleRequest request) =>
        request.Items.Select(i => new SaleItemValue(i.ProductId, i.Quantity)).ToList();
}
