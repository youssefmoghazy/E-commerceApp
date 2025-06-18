using Domain.Models.Basket;
using StackExchange.Redis;

namespace Persistance.Repositories;

public class BasketRepository(IConnectionMultiplexer connectionMultiplexer)
    : IBasketRepository
{
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
    public async Task<CustomerBasket> GetAsync(string id)
    {
        // Get object from DB
        var basket = await _database.StringGetAsync(id);
        // Deserialization
        if (basket.IsNullOrEmpty)
            return null!;
        // return
        return JsonSerializer.Deserialize<CustomerBasket>(basket!)!;
    }
    public async Task<CustomerBasket?> UpdateAsync(CustomerBasket basket, TimeSpan? TimeToLive = null)
    {
        var jsonBasket = JsonSerializer.Serialize(basket);
        var isCreatedOrUpdated = await _database.StringSetAsync(basket.id, jsonBasket, TimeToLive ?? TimeSpan.FromDays(3));
        return isCreatedOrUpdated ? await GetAsync(basket.id) : null;

    }
    public async Task<bool> DeleteAsync(string id) => await _database.KeyDeleteAsync(id);


}
