namespace Model
{
    public class WeatherModel
    {
        public TempInfo main { get; set; }
        public string name { get; set; }
        public WeatherInfo[] weather { get; set; } 
        
    
    }

    public class TempInfo
    {
        public float temp { get; set; }
    }

    public class WeatherInfo
    {
        public string description { get; set; }
    }

    public class WeatherResult
    {
        public string City { get; set;}
        public float Temp { get; set;}
        public string Description { get; set;}
    }
}

