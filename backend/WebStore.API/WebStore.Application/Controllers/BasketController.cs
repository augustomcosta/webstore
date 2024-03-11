﻿using Microsoft.AspNetCore.Mvc;
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

    [HttpGet]
    public async Task<IActionResult> GetBasketAsync(Guid id)
    {
        var basket = await _service.GetBasketAsync(id);
        return Ok(basket);
    }

    [HttpPut]
    public async Task<IActionResult> UpdateBasketAsync(Basket basket)
    {
        var updatedBasket = await _service.UpdateBasketAsync(basket);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteBasketAsync(Guid id)
    {
         await _service.DeleteBasketAsync(id);
         return Ok();
    }
}