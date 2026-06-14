using WebApi.Customers;

namespace WebApi.Discounts.Strategies;

public interface IDiscountStrategy
{
    public Discount? CalculateDiscount(Customer? customer, decimal saleAmount);
}