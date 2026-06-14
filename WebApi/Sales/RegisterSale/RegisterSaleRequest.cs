namespace WebApi.Sales.RegisterSale;

public record RegisterSaleRequest(
    string CustomerFullName,
    string CustomerNit,
    List<SaleItemRequest> Items);

public record SaleItemRequest(Guid ProductId, int Quantity);
