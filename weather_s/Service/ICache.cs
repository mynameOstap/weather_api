using Model;

namespace Service
{
    public interface ICache
    {
         Task<WeatherResult> GetJsonResponseAsync(string city);
    }
}

