using StackExchange.Redis;
using WebStore.Domain.Entities;
using WebStore.Domain.Repositories;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebStore.Data.RepositoriesImpl;

public class BasketRepository : IBasketRepository
{
    private readonly IDatabase _database;

    public BasketRepository(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<Basket> CreateBasketAsync(Guid userId)
    {
        var existingBasket = await GetBasketAsync(userId);
        if (existingBasket != null!) return existingBasket;

        var newBasket = new Basket(userId);

        await UpdateBasketAsync(userId, newBasket);

        return newBasket;
    }

    public async Task<Basket> GetBasketAsync(Guid basketId)
    {
        var data = await _database.StringGetAsync(basketId.ToString());

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
    }

    public async Task<Basket> UpdateBasketAsync(Guid basketId, Basket basket)
    {
        var created =
            await _database.StringSetAsync(basketId.ToString(), JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));
        if (!created) throw new Exception("Couldn't find basket");

        return await GetBasketAsync(basketId);
    }

    public async Task<bool> Delete(Guid basketId)
    {
        return await _database.KeyDeleteAsync(basketId.ToString());
    }
}