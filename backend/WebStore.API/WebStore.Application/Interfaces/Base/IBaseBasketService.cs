using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces.Base;

public interface IBaseBasketService<T>
{
    Task<T> CreateBasketAsync(string userId);
    Task<T> GetBasketAsync(string id);
    Task<T> UpdateBasketAsync(string id, Basket basket);
    Task DeleteBasketAsync(string id);
}