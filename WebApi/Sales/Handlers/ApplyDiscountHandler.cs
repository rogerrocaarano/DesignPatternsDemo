using WebApi.Discounts;
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

        // A brand-new customer is exposed as null so the first-purchase strategy applies.
        var customerForDiscount = context.IsNewCustomer ? null : sale.Customer;
        context.SaleDiscount = _discountService.GetDiscount(customerForDiscount, sale.CalculateTotal());

        await base.HandleAsync(context);
    }
}