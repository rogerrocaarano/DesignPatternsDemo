using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public abstract class SaleBaseHandler
{
    private SaleBaseHandler? _next;

    public virtual async Task HandleAsync(SaleContext context)
    {
        if (_next is null) return;
        await _next.HandleAsync(context);
    }

    public SaleBaseHandler SetNext(SaleBaseHandler next)
    {
        _next = next;
        return next;
    }
}