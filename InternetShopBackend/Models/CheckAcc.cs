using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShopBackend.Models;

public class CheckAcc : Attribute, IAuthorizationFilter
{
    public AppDbContext _Context;
    public CheckAcc(AppDbContext context)
    {
        _Context = context;
    }
    
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        foreach (var item in _Context.Accounts.ToList())
        {
            if(item.IsBan != null && item.IsBan)
                context.Result = new ObjectResult(new {Message = "Ваш аккаунт забанен, обратитесь к администратору."});
        }
    }
}