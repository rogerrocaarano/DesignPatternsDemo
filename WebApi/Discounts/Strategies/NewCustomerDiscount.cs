using WebApi.Customers;

namespace WebApi.Discounts.Strategies;

public class NewCustomerDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(Customer? customer, decimal saleAmount)
    {
        if (customer is not null) return null;

        const string message = "Se aplica descuento del 5% por primera compra.";
        var amount = 0.05m * saleAmount;
        return new Discount(message, amount);
    }
}