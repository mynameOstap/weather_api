using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Data;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
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
          
            var data = await _cachedResponseService.GetJsonResponseAsync(city);
              
            if (data == null)
            {
                return BadRequest("Не вдалося отримати дані про погоду.");
            }
                

                return Ok(data);

            
           
        }

    }
    [ApiController]
    [Route("/{controller}")]
    public class WeatherDbController : ControllerBase
    {
        private readonly WeatherContext _context;

        public WeatherDbController(WeatherContext context)
        {
            _context = context;
        }

        [HttpGet] public async Task<ActionResult<IEnumerable<WeatherResult>>> Get()
        {
            var dblast10 = await _context.WeatherResults.OrderBy(w => w.Inserted).Take(10).ToListAsync();
            return Ok(dblast10);
        }
    }
}

