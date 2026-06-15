namespace WebApi.Discounts.Strategies;

public class NewCustomerDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount)
    {
        const string message = "Se aplica descuento del 5% por primera compra.";
        var amount = 0.05m * saleAmount;
        return new Discount(message, amount);
    }
}
