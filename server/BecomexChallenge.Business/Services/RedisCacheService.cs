using BecomexChallenge.Domain.Interfaces;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace BecomexChallenge.Business.Services
{
    public class RedisCacheService : ICache
    {
        private readonly IDatabase _instance;

        public RedisCacheService(IConnectionMultiplexer connection)
        {
            _instance = connection.GetDatabase();
        }

        public async Task<string> GetCachedResult(string key)
        {
            return await _instance.StringGetAsync(key);
        }

        public async Task SetResultInCache(string key, string result)
        {
            await _instance.StringSetAsync(key, result);
        }
    }
}
