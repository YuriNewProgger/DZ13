using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;
using Serilog;

namespace InternetShopBackend.Models;

public class LogResourceFilter : Attribute, IResourceFilter
{
    private Stopwatch sw;

    public LogResourceFilter()
    {
        sw = new Stopwatch();
    }
    
    public void OnResourceExecuting(ResourceExecutingContext context)
    {
        sw.Start();
    }

    public void OnResourceExecuted(ResourceExecutedContext context)
    {
        sw.Stop();
        Log.Information($"Time spent - {sw.Elapsed}");
    }
}