
using System.Text.Json;

namespace Services;

public class CacheService(IcacheRepository cacheRepository)
    : ICacheService
{
    public async Task<string?> GetAsync(string cacheKey)
        => await cacheRepository.GetAsync(cacheKey);

    public async Task SetAsync(string cacheKey, object value, TimeSpan expiration)
    {
        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
        await cacheRepository.setAsync(cacheKey, JsonSerializer.Serialize(value), expiration);
    }
}
