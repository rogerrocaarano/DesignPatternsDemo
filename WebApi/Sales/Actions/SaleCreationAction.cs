namespace WebApi.Sales.Actions;

public record SaleCreationAction(string CustomerFullName, string CustomerNit, List<SaleItemValue> Items);
