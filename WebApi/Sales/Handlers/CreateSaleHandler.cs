using WebApi.Products;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class CreateSaleHandler : SaleBaseHandler
{
    private readonly IProductsRepository _productsRepository;

    public CreateSaleHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public override async Task HandleAsync(SaleContext context)
    {
        var customer = context.CustomerEntity!;
        var sale = new Sale(customer);

        foreach (var item in context.SaleRequest.Items)
        {
            var product = await _productsRepository.SearchProductByIdAsync(item.ProductId)
                          ?? throw new ProductNotFoundException(item.ProductId);

            sale.AddProduct(product, item.Quantity);
        }

        context.SaleEntity = sale;
        await base.HandleAsync(context);
    }
}