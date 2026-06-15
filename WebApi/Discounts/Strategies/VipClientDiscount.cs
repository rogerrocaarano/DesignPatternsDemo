namespace WebApi.Discounts.Strategies;

public class VipClientDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount)
    {
        const string message = "Se aplica descuento del 8% a cliente VIP";
        var amount = 0.08m * saleAmount;
        return new Discount(message, amount);
    }
}
