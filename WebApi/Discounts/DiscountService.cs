using WebApi.Discounts.Strategies;

namespace WebApi.Discounts;

public class DiscountService
{
    private IDiscountStrategy? _strategy;

    public void SetStrategy(IDiscountStrategy strategy) => _strategy = strategy;

    public Discount? GetDiscount(decimal saleAmount)
    {
        return _strategy?.CalculateDiscount(saleAmount);
    }
}
