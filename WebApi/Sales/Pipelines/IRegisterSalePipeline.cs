namespace WebApi.Sales.Pipelines;

public interface IRegisterSalePipeline
{
    Task<SaleContext> HandleRequest(SaleRequest request);
}