using Microsoft.EntityFrameworkCore;

namespace InternetShopBackend.Models;

public class BasketRepository : IBasketRepository
{
    public AppDbContext _Context;

    public BasketRepository(AppDbContext context)
    {
        _Context = context;
    }

    public List<Basket> Get() => _Context.Baskets.ToList();

    public void Add(Basket basket) => _Context.Baskets.Add(basket);
    
    public void Update(Basket basket) => _Context.Entry(basket).State = EntityState.Modified;

    public void Delete(Basket basket) => _Context.Baskets.Remove(basket);
}