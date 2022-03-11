using InternetShopBackend.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Controllers;

[Route("Products")]
[ApiController]
public class ProductController : ControllerBase
{
    private AppDbContext context;

    public ProductController(AppDbContext _context)
    {
        context = _context;
    }

    [HttpGet("GetProducts")]
    public IEnumerable<Product> GetProducts() => context.Products.ToList();

    [HttpPost("AddProduct")]
    public void AddProduct(Product _product)
    {
        Product newProduct = _product;
        newProduct.Id = 0;

    }
}