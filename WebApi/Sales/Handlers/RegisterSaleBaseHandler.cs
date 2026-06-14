using WebApi.Sales.Pipelines;

namespace WebApi.Sales.Handlers;

public class RegisterSaleBaseHandler
{
    private RegisterSaleBaseHandler? Next;

    public virtual async Task HandleAsync(SaleContext context)
    {
        if (Next is null) return;
        await Next.HandleAsync(context);
    }

    public RegisterSaleBaseHandler HandleNext(RegisterSaleBaseHandler next)
    {
        Next = next;
        return next;
    }
}