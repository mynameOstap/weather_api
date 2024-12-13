namespace Service
{
    public class GetWeather
    {
      
        public  async Task<string> WeatherApi(string city)
        {
            using (HttpClient client = new HttpClient())
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid={api}";
               
                HttpResponseMessage response = await client.GetAsync(url);
                string responseBody = await response.Content.ReadAsStringAsync();
                return responseBody;

            }
        }
    }
}

