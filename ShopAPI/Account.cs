using System.ComponentModel.DataAnnotations;

namespace ShopAPI;

public class Account
{
    [Required]
    [MinLength(5)]
    public string Login { get; set; }
    [Required]
    public string Password { get; set; }
    [Required]
    public  string Name { get; set; }
    public string Email { get; set; }
}