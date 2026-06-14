using WebApi.Sales.Pipelines;

namespace WebApi.Sales;

public static class SaleEndpoints
{
    public static IEndpointRouteBuilder MapSaleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sales").WithTags("Sales");

        group.MapPost("/discount", CalculateSaleDiscount)
            .WithName("CalculateSaleDiscount")
            .WithSummary("Calcula el descuento de una venta simulada (no la registra).");

        group.MapPost("/", RegisterSale)
            .WithName("RegisterSale")
            .WithSummary("Registra una venta completa, incluyendo los descuentos aplicados.");

        return app;
    }

    private static async Task<IResult> CalculateSaleDiscount(SaleRequest request, RegisterSalePipeline pipeline)
    {
        if (request.Items is null || request.Items.Count == 0)
            return Results.BadRequest("La venta debe incluir al menos un producto.");

        try
        {
            var context = await pipeline.HandleRequest(request);
            return Results.Ok(SaleResultDto.FromContext(context, false));
        }
        catch (ProductNotFoundException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }

    private static async Task<IResult> RegisterSale(SaleRequest request, SimulateSalePipeline pipeline)
    {
        if (request.Items is null || request.Items.Count == 0)
            return Results.BadRequest("La venta debe incluir al menos un producto.");

        try
        {
            var context = await pipeline.HandleRequest(request);
            var result = SaleResultDto.FromContext(context, true);
            return Results.Created($"/sales/{result.SaleId}", result);
        }
        catch (ProductNotFoundException ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
}