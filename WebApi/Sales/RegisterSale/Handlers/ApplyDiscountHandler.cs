using WebApi.Discounts;

namespace WebApi.Sales.RegisterSale.Handlers;

public class ApplyDiscountHandler(DiscountService discountService) : RegisterSaleBaseHandler
{
    public override async Task HandleAsync(RegisterSaleContext context)
    {
        var sale = context.SaleEntity;
        var discount = discountService.GetDiscount(sale.Customer, sale.CalculateTotal());
        
        context.SaleDiscount = discount;
        await base.HandleAsync(context);
    }
}