using WebStore.Domain.Entities;

namespace WebStore.API.Interfaces.Base;

public interface IBaseBasketService<T>
{
    Task<T> GetBasketAsync(Guid id);
    Task<T> UpdateBasketAsync(Basket basket);
    Task DeleteBasketAsync(Guid id);
}