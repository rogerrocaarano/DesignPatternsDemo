namespace WebApi.Products;

public static class ProductEndpoints
{
    public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("/products").WithTags("Products");

        group.MapGet("/", async (IProductsRepository repository) =>
            {
                var products = await repository.ListAllProductsAsync();
                return Results.Ok(products.Select(ProductDto.FromEntity));
            })
            .WithName("ListProducts")
            .WithSummary("Lista todos los productos.");

        group.MapGet("/ids", async (IProductsRepository repository) =>
                Results.Ok(await repository.ListAllProductIdsAsync()))
            .WithName("ListProductIds")
            .WithSummary("Lista los IDs de los productos.");

        group.MapGet("/{id:guid}", async (Guid id, IProductsRepository repository) =>
            {
                var product = await repository.SearchProductByIdAsync(id);
                return product is null
                    ? Results.NotFound()
                    : Results.Ok(ProductDto.FromEntity(product));
            })
            .WithName("GetProduct")
            .WithSummary("Obtiene el detalle de un producto.");

        return app;
    }
}