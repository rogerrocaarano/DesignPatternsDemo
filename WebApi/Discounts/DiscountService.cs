using WebApi.Customers;
using WebApi.Discounts.Strategies;
using WebApi.Sales;

namespace WebApi.Discounts;

public class DiscountService(IEnumerable<IDiscountStrategy> strategies)
{
    public Discount? GetDiscount(Customer customer, decimal saleAmount)
    {
        if (!strategies.Any()) return null;
        
        return strategies
            .Select(strategy => strategy.CalculateDiscount(customer, saleAmount))
            .Where(discount => discount != null)
            .OrderByDescending(discount => discount!.Amount)
            .FirstOrDefault();
    }
}