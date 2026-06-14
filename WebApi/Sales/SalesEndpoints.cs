using WebApi.Sales.Pipelines;

namespace WebApi.Sales;

public static class SalesEndpoints
{
    public static IEndpointRouteBuilder MapSaleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sales")
            .WithTags("Sales")
            .AddEndpointFilter(async (context, next) =>
            {
                var request = context.GetArgument<SaleRequest>(0);
                if (request?.Items is null || request.Items.Count == 0)
                {
                    return Results.BadRequest("La venta debe incluir al menos un producto.");
                }
                return await next(context);
            });

        group.MapPost("/discount", CalculateSaleDiscount)
            .WithName("CalculateSaleDiscount")
            .WithSummary("Calcula el descuento de una venta simulada (no la registra).");

        group.MapPost("/", RegisterSale)
            .WithName("RegisterSale")
            .WithSummary("Registra una venta completa, incluyendo los descuentos aplicados.");

        return app;
    }

    private static async Task<IResult> CalculateSaleDiscount(SaleRequest request, CalculateDiscountPipeline pipeline)
    {
        var context = await pipeline.HandleRequest(request);
        return Results.Ok(SaleResultDto.FromContext(context, false));
    }

    private static async Task<IResult> RegisterSale(SaleRequest request, SalePipeline pipeline)
    {
        var context = await pipeline.HandleRequest(request);
        var result = SaleResultDto.FromContext(context, true);
        return Results.Created($"/sales/{result.SaleId}", result);
    }
}