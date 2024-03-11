using WebStore.Domain.Entities;

namespace WebStore.Domain.Repositories;

public interface IBasketRepository
{
    Task<Basket> GetBasketAsync(Guid basketId);
    Task<Basket> UpdateBasketAsync(Basket basket);
    Task<bool> Delete(Guid basketId);
}