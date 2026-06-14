namespace WebApi.Sales.RegisterSale;

public class ProductNotFoundException(Guid productId)
    : Exception($"Product with id '{productId}' was not found.")
{
    public Guid ProductId { get; } = productId;
}
