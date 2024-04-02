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

    public async Task<Basket> CreateBasketAsync(string basketId)
    {
        var existingBasket = await GetBasketAsync(basketId);
        if (existingBasket != null!) return existingBasket;

        var newBasket = new Basket(basketId);

        await UpdateBasketAsync(basketId, newBasket);

        return newBasket;
    }

    public async Task<Basket> GetBasketAsync(string basketId)
    {
        var data = await _database.StringGetAsync(basketId);

        return data.IsNullOrEmpty ? null : JsonSerializer.Deserialize<Basket>(data);
    }

    public async Task<Basket> UpdateBasketAsync(string basketId, Basket basket)
    {
        var created =
            await _database.StringSetAsync(basketId, JsonSerializer.Serialize(basket),
                TimeSpan.FromDays(30));
        if (!created) throw new Exception("Couldn't find basket");

        return await GetBasketAsync(basketId);
    }

    public async Task<bool> Delete(string basketId)
    {
        return await _database.KeyDeleteAsync(basketId);
    }
}