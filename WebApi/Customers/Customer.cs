namespace WebApi.Customers;

public class Customer
{
    public Guid Id { get; private set; }
    public string Nit { get; private set; } = null!;
    public string FullName { get; private set; } = null!;
    public decimal TotalSales { get; private set; }

    private Customer()
    {
        // For ORM
    }

    public Customer(string nit, string fullName)
    {
        Id = Guid.NewGuid();
        Nit = nit;
        FullName = fullName;
        TotalSales = 0;
    }

    public void IncreaseTotalSales(decimal amount)
    {
        TotalSales += amount;
    }
}