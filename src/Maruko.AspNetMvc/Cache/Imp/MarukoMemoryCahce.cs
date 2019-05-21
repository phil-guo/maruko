using System;
using Microsoft.Extensions.Caching.Memory;

namespace Maruko.AspNetMvc.Cache.Imp
{
    public class MarukoMemoryCahce : IMarukoCache
    {
        private IMemoryCache _cache;

        public MarukoMemoryCahce(IMemoryCache cache)
        {
            _cache = cache;
        }

        public bool Exists(string cacheKey)
        {
            object ReturnValue;
            if (string.IsNullOrEmpty(cacheKey))
            {
                return false;
            }
            else
            {
                return _cache.TryGetValue(cacheKey, out ReturnValue);
            }
        }

        public object Get(string cacheKey)
        {
            return _cache.Get(cacheKey);
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.Get<T>(cacheKey);
        }

        public void Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
        }

        public T Set<T>(string cacheKey, T cachevalue)
        {
           return _cache.Set(cacheKey, cachevalue);
        }

        public T Set<T>(string cacheKey, T cachevalue, DateTimeOffset absoluteExpiration)
        {
            return _cache.Set<T>(cacheKey, cachevalue, absoluteExpiration);
        }

        public T Set<T>(string cacheKey, T cachevalue, TimeSpan absoluteExpirationRelativeToNow)
        {
            return _cache.Set<T>(cacheKey, cachevalue, absoluteExpirationRelativeToNow);
        }

        public T Set<T>(string cacheKey, T cachevalue, MemoryCacheEntryOptions options)
        {
            return _cache.Set<T>(cacheKey, cachevalue, options);
        }

        public bool TryGetValue<T>(string cacheKey, out T cachevalue)
        {
            return _cache.TryGetValue(cacheKey,out cachevalue);
        }
    }
}
