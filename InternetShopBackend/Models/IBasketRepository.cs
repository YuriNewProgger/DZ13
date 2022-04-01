namespace InternetShopBackend.Models;

public interface IBasketRepository : IRepository<Basket>
{
    Basket GetBasketByIdAcc(int id);
}