using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Models;

public class AppDbContext : DbContext
{
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Basket> Baskets => Set<Basket>();

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}

}