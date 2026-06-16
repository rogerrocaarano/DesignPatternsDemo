namespace WebApi.Sales.Actions;

public record CalculateDiscountAction(string CustomerFullName, string CustomerNit, List<SaleItemValue> Items);
