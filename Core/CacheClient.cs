using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace pokelist.Core
{
    public class CacheClient
    {
        private readonly IDistributedCache _cache;
        
        public CacheClient(IDistributedCache cache)
        {
            _cache = cache;
        }
        public async Task<T> ReadOrUpdateCache<T>(string cacheKey, Func<Task<T>> readMethod)
        {
            var values = await _cache.GetStringAsync(cacheKey);
            T item = default(T);
            if (values == null)
            {
                item = await readMethod();
                var cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                var cacheEntries = JsonConvert.SerializeObject(item);
                await _cache.SetStringAsync(cacheKey, cacheEntries, cacheOptions);
            } 
            else
            {
                item = JsonConvert.DeserializeObject<T>(values);
            }
            return item;
        }

        public async Task<T> ReadOrUpdateCache<T, TInput>(string cacheKey, Func<TInput, Task<T>> readMethod, TInput input)
        {
            var values = await _cache.GetStringAsync(cacheKey);
            T item = default(T);
            if (values == null)
            {
                item = await readMethod(input);
                var cacheOptions = new DistributedCacheEntryOptions();
                cacheOptions.SetAbsoluteExpiration(TimeSpan.FromMinutes(30));
                var cacheEntries = JsonConvert.SerializeObject(item);
                await _cache.SetStringAsync(cacheKey, cacheEntries, cacheOptions);
            } 
            else
            {
                item = JsonConvert.DeserializeObject<T>(values);
            }
            
            return item;
        }

    }
}