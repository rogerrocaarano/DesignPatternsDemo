namespace WebApi.Api.DTOs;

public record SaleResultItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitCost,
    int Quantity,
    decimal Subtotal);