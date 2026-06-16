using WebApi.Domain.Entities;
using WebApi.Domain.ValueObjects;
using WebApi.Sales.Actions;

namespace WebApi.Sales.Handlers;

public class SaleHandlerContext
{
    public required string CustomerFullName { get; set; }
    public required string CustomerNit { get; set; }
    public required List<SaleItemValue> Items { get; set; }
    public Customer? CustomerEntity { get; set; }
    public bool IsNewCustomer { get; set; }
    public Sale? SaleEntity { get; set; }
    public Discount? SaleDiscount { get; set; }

    public static SaleHandlerContext CreateFromAction(SaleCreationAction action) => new()
    {
        CustomerFullName = action.CustomerFullName,
        CustomerNit = action.CustomerNit,
        Items = action.Items
    };

    public static SaleHandlerContext CreateFromAction(CalculateDiscountAction action) => new()
    {
        CustomerFullName = action.CustomerFullName,
        CustomerNit = action.CustomerNit,
        Items = action.Items
    };
}
