using WebApi.Sales.Handlers;

namespace WebApi.Sales.Pipelines;

public class SalePipeline : ISalePipeline
{
    private readonly SaleBaseHandler _firstHandler;

    public SalePipeline(
        SearchOrCreateCustomerHandler searchOrCreateCustomer,
        CreateSaleHandler createSale,
        ApplyDiscountHandler applyDiscount,
        PersistSaleHandler persistSale)
    {
        searchOrCreateCustomer
            .SetNext(createSale)
            .SetNext(applyDiscount)
            .SetNext(persistSale);

        _firstHandler = searchOrCreateCustomer;
    }

    public async Task<SaleContext> HandleRequest(SaleRequest request)
    {
        var context = new SaleContext { SaleRequest = request };
        await _firstHandler.HandleAsync(context);
        return context;
    }
}