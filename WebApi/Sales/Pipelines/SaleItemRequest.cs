namespace WebApi.Sales.Pipelines;

public record SaleItemRequest(Guid ProductId, int Quantity);