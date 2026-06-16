using System.Text.Json.Serialization;
using WebApi.Domain.ValueObjects;
using WebApi.Sales.Handlers;

namespace WebApi.Sales.Results;

public record SaleCreationResult(
    Guid SaleId,
    [property: JsonPropertyName("subtotal")] decimal SubtotalBeforeDiscount,
    Discount? Discount)
{
    public static SaleCreationResult FromContext(SaleHandlerContext handlerContext)
    {
        var sale = handlerContext.SaleEntity!;
        var subtotal = sale.CalculateTotal();

        return new SaleCreationResult(
            sale.Id,
            subtotal,
            handlerContext.SaleDiscount);
    }
}
