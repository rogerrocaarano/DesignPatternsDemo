using WebApi.Customers;
using WebApi.Sales;

namespace WebApi.Discounts.Strategies;

public class NewCustomerDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(Customer? customer, decimal saleAmount)
    {
        if (customer is not null) return null;

        const string message = "Se aplica descuento de primera compra.";
        var amount = 0.05m * saleAmount;
        return new Discount(message, amount);
    }
}