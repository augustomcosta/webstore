using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using Microsoft.AspNetCore.Cors;

namespace WebStore.API.Controllers;

[EnableCors("AllowClient")]
[ApiController]
[Route("api/[controller]")]
public class BasketController : ControllerBase
{
    private readonly IBasketService _service;
    private readonly IMapper _mapper;

    public BasketController(IBasketService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetBasketAsync(string basketId)
    {
        var basketDto = await _service.GetBasketAsync(basketId);
        var basket = _mapper.Map<Basket>(basketDto);
        
        return Ok(basket ?? new Basket());
    }

    [HttpPut("update-basket")]
    public async Task<IActionResult> UpdateBasketAsync(Basket basket)
    {
        var updatedBasket = await _service.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketAsync(string basketId)
    {
        await _service.DeleteBasketAsync(basketId);
        return Ok();
    }
    
    [HttpGet("get-by-user")]
    public async Task<IActionResult> GetBasketByUserId(string userId)
    {
        var basket = await _service.GetBasketByUserId(userId);
        return Ok(basket);
    }

    [HttpGet("reset-user-basket")]
    public async Task<IActionResult> ResetUserBasket(string userId)
    {
        var basket = await _service.ResetUserBasket(userId);

        return Ok(basket);
    }
}