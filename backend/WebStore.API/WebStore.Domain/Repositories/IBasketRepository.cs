using WebStore.Domain.Entities;

namespace WebStore.Domain.Repositories;

public interface IBasketRepository
{
    Task<Basket> GetBasketAsync(string basketId);
    Task<Basket> UpdateBasketAsync(Basket basket);
    Task<Basket> Delete(string basketId);
    Task<Basket> GetBasketByUserId(string userId);
}