using WebApi.Products;

namespace WebApi.Api.DTOs;

public record ProductDto(Guid Id, string Name, decimal UnitCost)
{
    public static ProductDto FromEntity(Product product)
    {
        return new ProductDto(product.Id, product.Name, product.UnitCost);
    }
}