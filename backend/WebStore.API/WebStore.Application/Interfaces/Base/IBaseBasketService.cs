using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces.Base;

public interface IBaseBasketService<T>
{
    Task<T> GetBasketAsync(string id);
    Task<T> UpdateBasketAsync(Basket basket);
    Task DeleteBasketAsync(string id);
    Task<T> GetBasketByUserId(string userId);
}