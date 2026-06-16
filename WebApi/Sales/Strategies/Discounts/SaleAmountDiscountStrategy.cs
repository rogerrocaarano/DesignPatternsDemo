using WebApi.Domain.ValueObjects;

namespace WebApi.Sales.Strategies.Discounts;

public class SaleAmountDiscountStrategy : IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount)
    {
        const string message = "Se aplica descuento del 2% por compra mayor a Bs. 1000";
        var amount = 0.02m * saleAmount;
        return new Discount(message, amount);
    }
}
