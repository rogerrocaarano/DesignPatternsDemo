using WebApi.Sales.Handlers;

namespace WebApi.Sales.Pipelines;

public class SimulateSalePipeline(
    SearchOrCreateCustomerHandler searchOrCreateCustomer,
    CreateSaleHandler createSale,
    ApplyDiscountHandler applyDiscount) : IRegisterSalePipeline
{
    public async Task<SaleContext> HandleRequest(SaleRequest request)
    {
        searchOrCreateCustomer
            .HandleNext(createSale)
            .HandleNext(applyDiscount);

        var context = new SaleContext { SaleRequest = request };
        await searchOrCreateCustomer.HandleAsync(context);
        return context;
    }
}