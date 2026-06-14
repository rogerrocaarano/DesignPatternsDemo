using WebApi.Sales.Pipelines;

namespace WebApi.Sales;

public record SaleResultItemDto(
    Guid ProductId,
    string ProductName,
    decimal UnitCost,
    int Quantity,
    decimal Subtotal);

public record DiscountDto(string Message, decimal Amount);

public record SaleResultDto(
    Guid? SaleId,
    string CustomerNit,
    string CustomerFullName,
    bool IsNewCustomer,
    IReadOnlyList<SaleResultItemDto> Items,
    decimal Subtotal,
    DiscountDto? Discount,
    decimal Total)
{
    public static SaleResultDto FromContext(SaleContext context, bool persisted)
    {
        var sale = context.SaleEntity!;
        var customer = context.CustomerEntity!;

        var subtotal = sale.CalculateTotal();
        var discount = context.SaleDiscount;
        var total = subtotal - (discount?.Amount ?? 0m);

        var items = sale.Items
            .Select(item => new SaleResultItemDto(
                item.Item.Id,
                item.Item.Name,
                item.Item.UnitCost,
                item.Quantity,
                item.CalculateSubtotal()))
            .ToList();

        return new SaleResultDto(
            persisted ? sale.Id : null,
            customer.Nit,
            customer.FullName,
            context.IsNewCustomer,
            items,
            subtotal,
            discount is null ? null : new DiscountDto(discount.Message, discount.Amount),
            total);
    }
}