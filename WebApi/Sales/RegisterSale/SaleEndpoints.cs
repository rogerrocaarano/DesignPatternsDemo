namespace WebApi.Sales.RegisterSale;

public static class SaleEndpoints
{
    public static IEndpointRouteBuilder MapSaleEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/sales").WithTags("Sales");

        group.MapPost("/discount", async (RegisterSaleRequest request, RegisterSalePipeline pipeline) =>
            {
                if (request.Items is null || request.Items.Count == 0)
                {
                    return Results.BadRequest("La venta debe incluir al menos un producto.");
                }

                try
                {
                    var context = await pipeline.SimulateAsync(request);
                    return Results.Ok(SaleResultDto.FromContext(context, persisted: false));
                }
                catch (ProductNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("CalculateSaleDiscount")
            .WithSummary("Calcula el descuento de una venta simulada (no la registra).");

        group.MapPost("/", async (RegisterSaleRequest request, RegisterSalePipeline pipeline) =>
            {
                if (request.Items is null || request.Items.Count == 0)
                {
                    return Results.BadRequest("La venta debe incluir al menos un producto.");
                }

                try
                {
                    var context = await pipeline.RegisterAsync(request);
                    var result = SaleResultDto.FromContext(context, persisted: true);
                    return Results.Created($"/sales/{result.SaleId}", result);
                }
                catch (ProductNotFoundException ex)
                {
                    return Results.BadRequest(ex.Message);
                }
            })
            .WithName("RegisterSale")
            .WithSummary("Registra una venta completa, incluyendo los descuentos aplicados.");

        return app;
    }
}
