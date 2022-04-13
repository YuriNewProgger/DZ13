namespace InternetShopBackend;

public class CheckBrowserMiddleware
{
    private readonly RequestDelegate _next;
    public CheckBrowserMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        if (context.Request.Headers.UserAgent.Contains("Edge"))
        {
            await context.Response.WriteAsync("Supported only Edge browser!");
        }
        else
        {
            await _next(context);
        }
    }
}