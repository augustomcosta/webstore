using AutoMapper;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;

namespace WebStore.API.Controllers;

[EnableCors("AllowClient")]
[ApiController]
[Route("api/[controller]")]
public class WishlistController : ControllerBase
{
    private readonly IWishlistService _service;
    private readonly IMapper _mapper;

    public WishlistController(IWishlistService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetWishlistAsync(string wishlistId)
    {
        var wishlistDto = await _service.GetWishlistAsync(wishlistId);
        var wishlist = _mapper.Map<Wishlist>(wishlistDto);
        
        return Ok(wishlist ?? new Wishlist());
    }

    [HttpPost("update-wishlist")]
    public async Task<IActionResult> UpdateWishlistAsync(Wishlist wishlist)
    {
        var updatedBasket = await _service.UpdateWishlistAsync(wishlist);
        return Ok(updatedBasket);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteWishlistAsync(string wishlistId)
    {
        await _service.DeleteWishlistAsync(wishlistId);
        return Ok();
    }
}