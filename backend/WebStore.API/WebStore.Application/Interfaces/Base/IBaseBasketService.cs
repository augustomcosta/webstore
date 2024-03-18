using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces.Base;

public interface IBaseBasketService<T>
{
    Task<T> CreateBasketAsync(Guid userId);
    Task<T> GetBasketAsync(Guid id);
    Task<T> UpdateBasketAsync(Guid id, Basket basket);
    Task DeleteBasketAsync(Guid id);
}