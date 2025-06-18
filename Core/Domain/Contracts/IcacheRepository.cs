namespace Domain.Contracts;

public interface IcacheRepository
{
    Task<string?> GetAsync(string cacheKey);

    Task setAsync(string cacheKey, string value, TimeSpan expiration);
}
