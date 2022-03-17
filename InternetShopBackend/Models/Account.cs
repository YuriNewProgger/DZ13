namespace InternetShopBackend.Models;

public class Account
{
    public int Id { get; set; }
    public string Login { get; set; }
    public string HashePassword { get; set; }
    public  string Name { get; set; }
    public string Email { get; set; }
}