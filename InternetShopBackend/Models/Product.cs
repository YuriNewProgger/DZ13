using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopBackend.Models;

public class Product
{
    [Key, Required]
    public int Id { get; set; }
    [Required]
    public string Title { get; set; }
    [Required]
    public decimal Price { get; set; }
    [Required]
    public string Description = "Somthing description ...";
    
    public Product(){}

}