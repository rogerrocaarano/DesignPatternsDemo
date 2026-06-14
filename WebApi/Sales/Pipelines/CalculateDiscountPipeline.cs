using WebApi.Sales.Handlers;

namespace WebApi.Sales.Pipelines;

public class CalculateDiscountPipeline : ISalePipeline
{
    private readonly SaleBaseHandler _firstHandler;

    public CalculateDiscountPipeline(SearchOrCreateCustomerHandler searchOrCreateCustomer,
        CreateSaleHandler createSale,
        ApplyDiscountHandler applyDiscount)
    {
        searchOrCreateCustomer
            .SetNext(createSale)
            .SetNext(applyDiscount);
        
        _firstHandler = searchOrCreateCustomer;
    }
    
    public async Task<SaleContext> HandleRequest(SaleRequest request)
    {
        var context = new SaleContext { SaleRequest = request };
        await _firstHandler.HandleAsync(context);
        return context;
    }
}