using Microsoft.EntityFrameworkCore;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using WebStore.Infra.Context;

namespace WebStore.Data.RepositoriesImpl;

public class BasketRepository : IBasketRepository
{
    private readonly AppDbContext _context;
    public BasketRepository(AppDbContext context)
    {
       
        _context = context;
    }

    public async Task<Basket> CreateNewBasket(string userId)
    {
        var basket = new Basket
        {
            UserId = userId
        };

        await _context.Baskets!.AddAsync(basket);

        await _context.SaveChangesAsync();
        
        return basket;
    }
    

    public async Task<Basket> GetBasketAsync(string basketId)
    {
        var basket = await _context.Baskets!.FirstOrDefaultAsync(b => b.Id == basketId);
        if(basket is null)
        {
            throw new Exception($"Basket with Id {basketId} was not found");
        }

        return basket;
    }

    public async Task<Basket> UpdateBasketAsync(Basket basket)
    {
        if (basket.Id == null)
        {
            throw new ArgumentException("Basket ID cannot be null", nameof(basket.Id));
        }

        var basketToUpdate = await _context.Baskets!.FirstOrDefaultAsync(b => b.Id == basket.Id);
        if (basketToUpdate is null)
        {
            basketToUpdate = new Basket();
            basket.UpdateBasket(basketToUpdate);
            _context.Add(basketToUpdate);
            basketToUpdate.UpdateTotalPrice();
            await _context.SaveChangesAsync();
            return basketToUpdate;
        }

        basket.UpdateBasket(basketToUpdate);
        basketToUpdate.UpdateTotalPrice();

        await _context.SaveChangesAsync();

        return basketToUpdate;
    }

    public async Task<Basket> Delete(string basketId)
    {
        var basketToDelete = await GetBasketAsync(basketId);

        _context.Baskets!.Remove(basketToDelete);

        await _context.SaveChangesAsync();
        return basketToDelete;
    }

    public async Task<Basket> GetBasketByUserId(string userId){
        var basket = await _context.Baskets!.FirstOrDefaultAsync(b => b.UserId == userId);
        return basket!;
    }
}