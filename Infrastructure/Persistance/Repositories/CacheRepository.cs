﻿using Domain.Contracts;
using StackExchange.Redis;

namespace Persistance.Repositories;

public class CacheRepository(IConnectionMultiplexer connection)
    : IcacheRepository
{
    private readonly IDatabase _database = connection.GetDatabase();
    public async Task<string?> GetAsync(string cacheKey)
    {
        var value = await _database.StringGetAsync(cacheKey);

        return value.IsNullOrEmpty ? null : value.ToString();
    }

    public async Task setAsync(string cacheKey, string value, TimeSpan expiration)
        => await _database.StringSetAsync(cacheKey, value, expiration);
}
