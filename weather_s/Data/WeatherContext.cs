using Microsoft.EntityFrameworkCore;


namespace Data
{
    
    public class WeatherContext : DbContext
    {
        public WeatherContext(DbContextOptions<WeatherContext> options) : base(options)
        {
            
        }

        public DbSet<Model.WeatherResult> WeatherResults { get; set;}

    }
}
