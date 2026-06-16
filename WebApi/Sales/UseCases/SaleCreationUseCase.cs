using WebApi.Sales.Actions;
using WebApi.Sales.Handlers;
using WebApi.Sales.Results;

namespace WebApi.Sales.UseCases;

public class SaleCreationUseCase
{
    private readonly SaleBaseHandler _firstHandler;
    
    public SaleCreationUseCase(
        SearchOrCreateCustomerHandler searchOrCreateCustomer,
        CreateSaleHandler createSale,
        ApplyDiscountHandler applyDiscount,
        PersistSaleHandler persistSale)
    {
        _firstHandler = searchOrCreateCustomer;
        _firstHandler
            .SetNext(createSale)
            .SetNext(applyDiscount)
            .SetNext(persistSale);
    }
    
    public async Task<SaleCreationResult> RealizeAsync(SaleCreationAction action)
    {
        var context = SaleHandlerContext.CreateFromAction(action);
        await _firstHandler.HandleAsync(context);
        return SaleCreationResult.FromContext(context);
    }
}