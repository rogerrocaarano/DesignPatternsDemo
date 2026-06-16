using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebApi.Controllers.DTOs;

namespace WebApi.Controllers.Filters;

public class ValidateSaleRequestAttribute : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var request = context.ActionArguments.Values.OfType<CreateSaleRequest>().FirstOrDefault();
        if (request?.Items is null || request.Items.Count == 0)
        {
            context.Result = new BadRequestObjectResult("La venta debe incluir al menos un producto.");
            return;
        }

        await next();
    }
}
