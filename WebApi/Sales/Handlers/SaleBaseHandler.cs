namespace WebApi.Sales.Handlers;

public abstract class SaleBaseHandler
{
    private SaleBaseHandler? _next;

    public virtual async Task HandleAsync(SaleHandlerContext handlerContext)
    {
        if (_next is null) return;
        await _next.HandleAsync(handlerContext);
    }

    public SaleBaseHandler SetNext(SaleBaseHandler next)
    {
        _next = next;
        return next;
    }
}