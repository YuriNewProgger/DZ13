using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetShopBackend.Models;

public class Basket
{
    [Key]
    public int Id { get; set; }
    public int ProductId { get; set; }
    public int IdAcc { get; set; }
}