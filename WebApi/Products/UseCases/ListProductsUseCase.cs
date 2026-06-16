using WebApi.Domain.Repositories;
using WebApi.Products.Results;

namespace WebApi.Products.UseCases;

public class ListProductsUseCase
{
    private readonly IProductsRepository _repository;

    public ListProductsUseCase(IProductsRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<ProductResult>> RealizeAsync()
    {
        var products = await _repository.ListAllProductsAsync();
        return products.Select(ProductResult.FromEntity).ToList();
    }
}
