using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace InternetShopBackend.Models;

public class FilterException : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var message = GetMessageException(context);
        if (!string.IsNullOrEmpty(message))
        {
            context.Result = new ObjectResult(new {Message = message});
            context.ExceptionHandled = true;
        }
        else
            context.Result = new ObjectResult(new {Message = context.Exception.Message});
    }

    private string? GetMessageException(ExceptionContext context)
    {
        return context.Exception switch
        {
            ValidationException => "Некорректные данные",
            _ => null
        };
    } 
}