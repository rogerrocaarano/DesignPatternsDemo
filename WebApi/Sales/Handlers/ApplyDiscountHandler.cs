using WebApi.Discounts;
using WebApi.Discounts.Strategies;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class ApplyDiscountHandler : SaleBaseHandler
{
    private readonly DiscountService _discountService;

    public ApplyDiscountHandler(DiscountService discountService)
    {
        _discountService = discountService;
    }

    public override async Task HandleAsync(SaleContext context)
    {
        var sale = context.SaleEntity!;
        var total = sale.CalculateTotal();

        var strategy = SelectStrategy(context, total);

        if (strategy is null)
        {
            context.SaleDiscount = null;
        }
        else
        {
            _discountService.SetStrategy(strategy);
            context.SaleDiscount = _discountService.GetDiscount(total);
        }

        await base.HandleAsync(context);
    }

    private static IDiscountStrategy? SelectStrategy(SaleContext context, decimal total)
    {
        if (context.IsNewCustomer)
            return new NewCustomerDiscount();

        if (context.CustomerEntity?.TotalSales >= 10_000m)
            return new VipClientDiscount();

        if (total >= 1_000m)
            return new SaleAmountDiscount();

        return null;
    }
}
