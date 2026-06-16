using WebApi.Domain.ValueObjects;

namespace WebApi.Sales.Strategies.Discounts;

public interface IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount);
}
