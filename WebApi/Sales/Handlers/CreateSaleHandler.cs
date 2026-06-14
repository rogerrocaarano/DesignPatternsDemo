using WebApi.Products;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class CreateSaleHandler(IProductsRepository productsRepository) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(SaleContext context)
    {
        var customer = context.CustomerEntity!;
        var sale = new Sale(customer);

        foreach (var item in context.SaleRequest.Items)
        {
            var product = await productsRepository.SearchProductByIdAsync(item.ProductId)
                          ?? throw new ProductNotFoundException(item.ProductId);

            sale.AddProduct(product, item.Quantity);
        }

        context.SaleEntity = sale;
        await base.HandleAsync(context);
    }
}