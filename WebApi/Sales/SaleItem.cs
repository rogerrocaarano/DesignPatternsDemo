using WebApi.Products;

namespace WebApi.Sales;

public class SaleItem
{
    public Product Item { get; set; }
    public int Quantity { get; set; }

    public decimal CalculateSubtotal()
    {
        return Item.UnitCost * Quantity;
    }
}