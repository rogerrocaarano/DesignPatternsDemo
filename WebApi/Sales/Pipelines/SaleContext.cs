using WebApi.Customers;
using WebApi.Discounts;

namespace WebApi.Sales.Pipelines;

public class SaleContext
{
    public required SaleRequest SaleRequest { get; set; }
    public Customer? CustomerEntity { get; set; }
    public bool IsNewCustomer { get; set; }
    public Sale? SaleEntity { get; set; }
    public Discount? SaleDiscount { get; set; }
}