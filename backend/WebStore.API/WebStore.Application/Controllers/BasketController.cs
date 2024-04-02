using Microsoft.AspNetCore.Mvc;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;

namespace WebStore.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _service;

    public BasketController(IBasketService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> CreateBasketAsync(string basketId)
    {
        var basket = await _service.CreateBasketAsync(basketId);
        return Ok(basket);
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketAsync(string basketId)
    {
        var basket = await _service.GetBasketAsync(basketId);
        return Ok(basket);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasketAsync(string basketId, Basket basket)
    {
        var updatedBasket = await _service.UpdateBasketAsync(basketId, basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketAsync(string basketId)
    {
        await _service.DeleteBasketAsync(basketId);
        return Ok();
    }
}