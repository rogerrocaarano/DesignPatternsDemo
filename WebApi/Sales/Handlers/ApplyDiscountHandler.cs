using WebApi.Discounts;
using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class ApplyDiscountHandler(DiscountService discountService) : SaleBaseHandler
{
    public override async Task HandleAsync(SaleContext context)
    {
        var sale = context.SaleEntity!;

        // A brand-new customer is exposed as null so the first-purchase strategy applies.
        var customerForDiscount = context.IsNewCustomer ? null : sale.Customer;
        context.SaleDiscount = discountService.GetDiscount(customerForDiscount, sale.CalculateTotal());

        await base.HandleAsync(context);
    }
}