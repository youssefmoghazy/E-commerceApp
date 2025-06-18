namespace ServicesAbstractions;

public interface ICacheService
{
    Task<string?> GetAsync(string cacheKey);
    Task SetAsync(string cacheKey, object value, TimeSpan expiration);
}
