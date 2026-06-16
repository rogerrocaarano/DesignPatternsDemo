using WebApi.Domain.Repositories;
using WebApi.Products.Actions;
using WebApi.Products.Results;

namespace WebApi.Products.UseCases;

public class GetProductUseCase
{
    private readonly IProductsRepository _repository;

    public GetProductUseCase(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task<ProductResult?> RealizeAsync(GetProductAction action)
    {
        var product = await _repository.SearchProductByIdAsync(action.Id);
        return product is null ? null : ProductResult.FromEntity(product);
    }
}
