using WebApi.Customers;
using WebApi.Products;

namespace WebApi.Sales;

public class Sale
{
    private readonly List<SaleItem> _items = [];

    private Sale()
    {
        // For ORM
    }

    public Sale(Customer customer)
    {
        Id = Guid.NewGuid();
        Customer = customer;
        _items = [];
    }

    public Guid Id { get; private set; }
    public Customer Customer { get; private set; } = null!;
    public IEnumerable<SaleItem> Items => _items;

    public decimal CalculateTotal()
    {
        return _items.Sum(item => item.CalculateSubtotal());
    }

    public void AddProduct(Product product, int quantity)
    {
        if (quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");

        _items.Add(new SaleItem { Item = product, Quantity = quantity });
    }
}