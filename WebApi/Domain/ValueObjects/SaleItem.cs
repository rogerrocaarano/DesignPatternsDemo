using WebApi.Domain.Entities;

namespace WebApi.Domain.ValueObjects;

public class SaleItem
{
    public Product Item { get; set; } = null!;
    public int Quantity { get; set; }

    public decimal CalculateSubtotal()
    {
        return Item.UnitCost * Quantity;
    }
}