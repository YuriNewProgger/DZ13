using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace InternetShopBackend.Models;

public class ProfileFilter : Attribute, IAsyncActionFilter
{
    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        Stopwatch sw = new Stopwatch();
        sw.Start();
        await next();
        sw.Stop();
        Log.Warning($"Profile {context.Controller.ToString()} /time spent{sw.Elapsed}");
    }
}