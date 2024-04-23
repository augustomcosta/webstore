using WebStore.API.DTOs;
using WebStore.API.DTOs.BasketDtoAggregate;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces;

public interface IBasketService : IBaseBasketService<BasketDto>
{
    Task<BasketUpdateDto> UpdateBasketAsync(Basket basket);
}