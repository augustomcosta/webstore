using WebStore.Domain.Entities;

namespace WebStore.Domain.Repositories;

public interface IBasketRepository
{
    Task<Basket> CreateBasketAsync(string basketId);
    Task<Basket> GetBasketAsync(string basketId);
    Task<Basket> UpdateBasketAsync(string basketId, Basket basket);
    Task<bool> Delete(string basketId);
}