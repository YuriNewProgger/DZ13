using System.ComponentModel.Design;
using System.Threading.Tasks;
using InternetShopBackend;
using Microsoft.AspNetCore.Http;
using Xunit;

namespace InternetShopBack.Test;

public class UnitTest1
{
    [Fact]
    public async Task Check_Browser()
    {
        var passed = false;
        var middleWare = new CheckBrowserMiddleware(_=>
        {
            passed = true;
            return  Task.CompletedTask;
        });

        var httpContext = new DefaultHttpContext();
        httpContext.Request.Headers.Add("User-Agent", "Chrome");

        await middleWare.InvokeAsync(httpContext);
        
        Assert.True(passed);
    }
}