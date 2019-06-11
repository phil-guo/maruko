using System;
using Maruko.Dependency;
using Microsoft.Extensions.Caching.Memory;

namespace Maruko.Runtime.Cache
{
    public interface IMkoCache : IDependencySingleton
    {
        T Set<T>(string cacheKey, T cachevalue);
        T Set<T>(string cacheKey, T cachevalue, DateTimeOffset absoluteExpiration);
        T Set<T>(string cacheKey, T cachevalue, TimeSpan absoluteExpirationRelativeToNow);
        T Set<T>(string cacheKey, T cachevalue, MemoryCacheEntryOptions options);
        object Get(string cacheKey);
        T Get<T>(string cacheKey);
        bool TryGetValue<T>(string cacheKey, out T cachevalue);

        void Remove(string cacheKey);

        bool Exists(string cacheKey);
    }
}
