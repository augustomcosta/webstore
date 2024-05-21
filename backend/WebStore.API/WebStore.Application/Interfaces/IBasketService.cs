using WebStore.API.DTOs.BasketDtoAggregate;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces;

public interface IBasketService : IBaseBasketService<BasketDto>
{
    Task<BasketDto> UpdateBasketAsync(Basket basket);
    Task<BasketDto> ResetUserBasket(string userId);
}