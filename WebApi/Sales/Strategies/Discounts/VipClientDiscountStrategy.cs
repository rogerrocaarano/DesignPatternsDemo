using WebApi.Domain.ValueObjects;

namespace WebApi.Sales.Strategies.Discounts;

public class VipClientDiscountStrategy : IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount)
    {
        const string message = "Se aplica descuento del 8% a cliente VIP";
        var amount = 0.08m * saleAmount;
        return new Discount(message, amount);
    }
}
