using WebApi.Sales.Actions;
using WebApi.Sales.Handlers;
using WebApi.Sales.Results;

namespace WebApi.Sales.UseCases;

public class CalculateDiscountUseCase
{
    private readonly SaleBaseHandler _firstHandler;

    public CalculateDiscountUseCase(
        SearchOrCreateCustomerHandler searchOrCreateCustomer,
        CreateSaleHandler createSale,
        ApplyDiscountHandler applyDiscount)
    {
        _firstHandler = searchOrCreateCustomer;
        _firstHandler
            .SetNext(createSale)
            .SetNext(applyDiscount);
    }

    public async Task<CalculateDiscountResult> RealizeAsync(CalculateDiscountAction action)
    {
        var context = SaleHandlerContext.CreateFromAction(action);
        await _firstHandler.HandleAsync(context);
        return CalculateDiscountResult.FromContext(context);
    }
}
