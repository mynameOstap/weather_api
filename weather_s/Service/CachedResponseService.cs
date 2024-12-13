using System;
using System.Text.Json;
using Data;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace Service
{
    public class CachedResponseService : ICache
    {
        private readonly IMemoryCache _cache;
        private readonly TimeSpan _cacheDuration = TimeSpan.FromMinutes(5);
        private readonly GetWeather _weatherApi;
        private readonly WeatherContext _context;
        public CachedResponseService(IMemoryCache cache, GetWeather weatherApi,WeatherContext context)
        {
            _cache = cache;
            _weatherApi = weatherApi;
            _context = context;
        }

     

        public async Task<WeatherResult> GetJsonResponseAsync(string city)  
        {
            string cacheKey = $"jsonResponse_{city.Trim().ToLower()}";
            if (_cache.TryGetValue(cacheKey, out WeatherResult cachedResponse))
            {
                Console.WriteLine("Отримано з кешу");
                return cachedResponse;
            }
            Console.WriteLine("Оновлення кешу...");
            var freshResponse = await _weatherApi.WeatherApi(city);
            var weatherData = JsonSerializer.Deserialize<WeatherModel>(freshResponse);
            var data = new WeatherResult
            {
                City = weatherData.name,
                Description = weatherData.weather[0].description,
                Temp = weatherData.main.temp
            };
            _context.WeatherResults.Add(data);
            await _context.SaveChangesAsync();
            CacheResponse(cacheKey, data);
            return data;
        }

       

        private void CacheResponse(string cacheKey, WeatherResult data)
        {
            _cache.Set(cacheKey, data, _cacheDuration);
        }
    }
    
}

// проміжний клас який буде провіряти якщо кеш є повертати якщо нема робити гет і писати кеш