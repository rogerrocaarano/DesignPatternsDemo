using WebApi.Domain.ValueObjects;

namespace WebApi.Sales.Strategies.Discounts;

public class DiscountStrategyContext
{
    private IDiscountStrategy? _strategy;

    public void SetStrategy(IDiscountStrategy strategy) => _strategy = strategy;

    public Discount? GetDiscount(decimal saleAmount)
    {
        return _strategy?.CalculateDiscount(saleAmount);
    }
}
