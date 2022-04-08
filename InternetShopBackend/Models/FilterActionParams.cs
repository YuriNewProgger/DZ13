using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace InternetShopBackend.Models;

public class FilterActionParams : IActionFilter
{
    public void OnActionExecuting(ActionExecutingContext context)
    {
        foreach (var (key, value) in context.ActionArguments)
        {
            Log.Information("[{Endpoint}] {Param}: {@Value}",
                context.ActionDescriptor.DisplayName, key, value);
        }

    }

    public void OnActionExecuted(ActionExecutedContext context)
    {
        throw new NotImplementedException();
    }
}