using InternetShopBackend.Models;
using Microsoft.AspNetCore.Mvc;

namespace InternetShopBackend.Controllers;

[Route("Baskets")]
[ApiController]
public class BasketController : ControllerBase
{
    public UnitOfWork _uow;
    public BasketController(UnitOfWork UOW)
    {
        _uow = UOW;
    }

    [HttpGet("GetBasket")]
    public Basket GetBasketById(int idAcc) => _uow.BasketRepository.GetBasketByIdAcc(idAcc);

    [HttpPost("Buy")]
    public ActionResult Buy(int idAcc, int idProduct)
    {
        Basket basket = _uow.BasketRepository.GetBasketByIdAcc(idAcc);
        basket.ProductId = idProduct;

        return Ok();
    }
}