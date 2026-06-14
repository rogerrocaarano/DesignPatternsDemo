namespace WebApi.Sales.Pipelines;

public interface ISalePipeline
{
    Task<SaleContext> HandleRequest(SaleRequest request);
}