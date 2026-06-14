using WebApi.Sales.RegisterSale.Handlers;

namespace WebApi.Sales.RegisterSale;

/// <summary>
/// Builds and runs the register-sale chain of responsibility. Exposes a full run
/// (persisting the sale) and a simulation (stops before persisting) so the discount
/// can be previewed without side effects.
/// </summary>
public class RegisterSalePipeline(
    SearchOrCreateCustomerHandler searchOrCreateCustomer,
    CreateSaleHandler createSale,
    ApplyDiscountHandler applyDiscount,
    PersistSaleHandler persistSale)
{
    public Task<RegisterSaleContext> SimulateAsync(RegisterSaleRequest request)
    {
        searchOrCreateCustomer
            .HandleNext(createSale)
            .HandleNext(applyDiscount);

        return RunAsync(request);
    }

    public Task<RegisterSaleContext> RegisterAsync(RegisterSaleRequest request)
    {
        searchOrCreateCustomer
            .HandleNext(createSale)
            .HandleNext(applyDiscount)
            .HandleNext(persistSale);

        return RunAsync(request);
    }

    private async Task<RegisterSaleContext> RunAsync(RegisterSaleRequest request)
    {
        var context = new RegisterSaleContext { RegisterSaleRequest = request };
        await searchOrCreateCustomer.HandleAsync(context);
        return context;
    }
}
