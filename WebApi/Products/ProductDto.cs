namespace WebApi.Products;

public record ProductDto(Guid Id, string Name, decimal UnitCost)
{
    public static ProductDto FromEntity(Product product) =>
        new(product.Id, product.Name, product.UnitCost);
}
