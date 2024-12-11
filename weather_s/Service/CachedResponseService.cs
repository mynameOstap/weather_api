using System;
using System.Text.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;

namespace Service
{
    public class CachedResponseService : ICache
    {
        private readonly IMemoryCache _cache;
       
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
        private readonly GetWeather _weatherApi;
        public CachedResponseService(IMemoryCache cache, GetWeather weatherApi)
        {
            _cache = cache;
            _weatherApi = weatherApi;
        }

        public async Task<string> GetJsonResponseAsync(string city)  
        {
            string cacheKey = $"jsonResponse_{city}"; 
            if (_cache.TryGetValue(cacheKey, out string cachedResponse))
            {
                Console.WriteLine("Отримано з кешу");
                return cachedResponse;
            }
            Console.WriteLine("Оновлення кешу...");
            var freshResponse = await _weatherApi.WeatherApi(city);
            CacheResponse(cacheKey, freshResponse);
            return freshResponse;
        }

       

        private void CacheResponse(string cacheKey, string jsonResponse)
        {
            _cache.Set(cacheKey, jsonResponse, _cacheDuration);
        }
    }
    
}

// проміжний клас який буде провіряти якщо кеш є повертати якщо нема робити гет і писати кеш