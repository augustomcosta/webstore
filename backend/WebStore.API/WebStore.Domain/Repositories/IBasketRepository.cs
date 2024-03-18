using WebStore.Domain.Entities;

namespace WebStore.Domain.Repositories;

public interface IBasketRepository
{
    Task<Basket> CreateBasketAsync(Guid basketId);
    Task<Basket> GetBasketAsync(Guid basketId);
    Task<Basket> UpdateBasketAsync(Guid basketId, Basket basket);
    Task<bool> Delete(Guid basketId);
}