using WebApi.Customers;
using WebApi.Discounts;

namespace WebApi.Sales.RegisterSale;

public class RegisterSaleContext
{
    public required RegisterSaleRequest RegisterSaleRequest { get; set; }
    public Customer? CustomerEntity { get; set; }
    public bool IsNewCustomer { get; set; }
    public Sale? SaleEntity { get; set; }
    public Discount? SaleDiscount { get; set; }
}
