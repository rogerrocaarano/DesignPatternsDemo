namespace WebApi.Sales.Pipelines;

public record SaleRequest(
    string CustomerFullName,
    string CustomerNit,
    List<SaleItemRequest> Items);