using WebApi.Customers;

namespace WebApi.Discounts.Strategies;

public class VipClientDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(Customer? customer, decimal saleAmount)
    {
        if (customer is null || customer.TotalSales < 10_000m) return null;

        const string message = "Se aplica descuento del 8% a cliente VIP";
        var amount = 0.08m * saleAmount;
        return new Discount(message, amount);
    }
}