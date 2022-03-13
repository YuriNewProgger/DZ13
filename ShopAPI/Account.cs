using System.ComponentModel.DataAnnotations;

namespace ShopAPI;

public class Account
{
    [MinLength(5)]
    public string Login { get; set; }
    public string Password { get; set; }
}