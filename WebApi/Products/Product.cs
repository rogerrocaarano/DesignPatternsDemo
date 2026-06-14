namespace WebApi.Products;

public class Product
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public decimal UnitCost { get; set; }
}