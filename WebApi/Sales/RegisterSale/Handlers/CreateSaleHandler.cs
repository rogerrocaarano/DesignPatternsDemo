namespace WebApi.Sales.RegisterSale.Handlers;

public class CreateSaleHandler() : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var sale = new Sale(context.CustomerEntity, context.RegisterSaleRequest.SaleItems);
        
        context.SaleEntity = sale;
        await base.HandleAsync(context);
    }
}