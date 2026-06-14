namespace WebApi.Sales.RegisterSale;

public record RegisterSaleRequest(string CustomerFullName, string CustomerNit, List<SaleItem> SaleItems);