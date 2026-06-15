namespace WebApi.Discounts.Strategies;

public interface IDiscountStrategy
{
    public Discount? CalculateDiscount(decimal saleAmount);
}
