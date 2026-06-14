namespace WebApi.Sales;

public record SaleResultItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitCost,
    int Quantity,
    decimal Subtotal);
