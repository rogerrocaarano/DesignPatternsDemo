using WebApi.Domain.Entities;

namespace WebApi.Products.Results;

public record ProductResult(Guid Id, string Name, decimal UnitCost)
{
    public static ProductResult FromEntity(Product product)
    {
        return new ProductResult(product.Id, product.Name, product.UnitCost);
    }
}
