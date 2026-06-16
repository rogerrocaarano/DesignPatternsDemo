using WebApi.Domain.Entities;
using WebApi.Domain.Exceptions;
using WebApi.Domain.Repositories;

namespace WebApi.Sales.Handlers;

public class CreateSaleHandler : SaleBaseHandler
{
    private readonly IProductsRepository _productsRepository;

    public CreateSaleHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public override async Task HandleAsync(SaleHandlerContext handlerContext)
    {
        var customer = handlerContext.CustomerEntity!;
        var sale = new Sale(customer);

        foreach (var item in handlerContext.Items)
        {
            var product = await _productsRepository.SearchProductByIdAsync(item.ProductId)
                          ?? throw new ProductNotFoundException(item.ProductId);

            sale.AddProduct(product, item.Quantity);
        }

        handlerContext.SaleEntity = sale;
        await base.HandleAsync(handlerContext);
    }
}