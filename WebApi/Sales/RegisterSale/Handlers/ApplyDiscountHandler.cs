using WebApi.Discounts;

namespace WebApi.Sales.RegisterSale.Handlers;

public class ApplyDiscountHandler(DiscountService discountService) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var sale = context.SaleEntity!;

        // A brand-new customer is exposed as null so the first-purchase strategy applies.
        var customerForDiscount = context.IsNewCustomer ? null : sale.Customer;
        context.SaleDiscount = discountService.GetDiscount(customerForDiscount, sale.CalculateTotal());

        await base.HandleAsync(context);
    }
}
