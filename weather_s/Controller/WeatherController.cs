using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Model;

namespace controller
{
    [ApiController]
    [Route("/{controller}")]
    public class WeatherController : ControllerBase
    {
            private readonly Service.ICache _cachedResponseService;

            public WeatherController(Service.ICache cachedResponseService)
            {
                _cachedResponseService = cachedResponseService;
            }
       

        [HttpGet]
        public async Task<ActionResult<WeatherResult>> Get(string city)
        {
          
            var responseBody = await _cachedResponseService.GetJsonResponseAsync(city);
            var weatherData = JsonSerializer.Deserialize<WeatherModel>(responseBody);
              
                if (weatherData == null)
                {
                    return BadRequest("Не вдалося отримати дані про погоду. Можливо, структура відповіді змінилася.");
                }

                if (weatherData.main == null || weatherData.weather == null || weatherData.weather.Length == 0)
                {
                    return BadRequest("Не вдалося отримати інформацію про температуру або погоду.");
                }

                var data = new WeatherResult
                {
                    City = weatherData.name,
                    Description = weatherData.weather[0].description,
                    Temp = weatherData.main.temp
                };

                return Ok(data);

            
           
        }

    }
}

