using WebApi.Customers;
using WebApi.Sales;

namespace WebApi.Discounts.Strategies;

public class SaleAmountDiscount : IDiscountStrategy
{
    public Discount? CalculateDiscount(Customer? customer, decimal saleAmount)
    {
        if (saleAmount < 1000m) return null;

        const string message = "Se aplica descuento de Bs. 100 por compra mayor a Bs. 1000";
        const decimal amount = 100m;
        return new Discount(message, amount);
    }
}