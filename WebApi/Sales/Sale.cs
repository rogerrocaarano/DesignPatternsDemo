using WebApi.Customers;
using WebApi.Products;

namespace WebApi.Sales;

public class Sale
{
    public Guid Id { get; set; }
    public Customer Customer { get; set; }
    public IEnumerable<SaleItem> Items { get; set; } = [];

    private Sale()
    {
        // For ORM
    }

    public Sale(Customer customer, IEnumerable<SaleItem> items)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        Items = items;
    }

    public decimal CalculateTotal()
    {
        throw new NotImplementedException();
    }

    public void AddProduct(Product product,  int quantity)
    {
        throw new NotImplementedException();
    }
}