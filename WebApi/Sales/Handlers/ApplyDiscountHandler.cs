using WebApi.Sales.Strategies.Discounts;

namespace WebApi.Sales.Handlers;

public class ApplyDiscountHandler : SaleBaseHandler
{
    private readonly DiscountStrategyContext _discountStrategyContext;

    public ApplyDiscountHandler(DiscountStrategyContext discountStrategyContext)
    {
        _discountStrategyContext = discountStrategyContext;
    }

    public override async Task HandleAsync(SaleHandlerContext handlerContext)
    {
        var sale = handlerContext.SaleEntity!;
        var total = sale.CalculateTotal();

        var strategy = SelectStrategy(handlerContext, total);

        if (strategy is null)
        {
            handlerContext.SaleDiscount = null;
        }
        else
        {
            _discountStrategyContext.SetStrategy(strategy);
            handlerContext.SaleDiscount = _discountStrategyContext.GetDiscount(total);
        }

        await base.HandleAsync(handlerContext);
    }

    private static IDiscountStrategy? SelectStrategy(SaleHandlerContext handlerContext, decimal total)
    {
        if (handlerContext.IsNewCustomer)
            return new NewCustomerDiscountStrategy();

        if (handlerContext.CustomerEntity?.TotalSales >= 10_000m)
            return new VipClientDiscountStrategy();

        if (total >= 1_000m)
            return new SaleAmountDiscountStrategy();

        return null;
    }
}
