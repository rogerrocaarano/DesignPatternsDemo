using System.Text.Json.Serialization;
using WebApi.Domain.ValueObjects;
using WebApi.Sales.Handlers;

namespace WebApi.Sales.Results;

public record CalculateDiscountResult(
    [property: JsonPropertyName("subtotal")] decimal SubtotalBeforeDiscount,
    Discount? Discount)
{
    public static CalculateDiscountResult FromContext(SaleHandlerContext context)
    {
        var subtotal = context.SaleEntity!.CalculateTotal();
        return new CalculateDiscountResult(subtotal, context.SaleDiscount);
    }
}
