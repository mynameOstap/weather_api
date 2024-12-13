using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;


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
    [Table("Weather")] public class WeatherResult
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [JsonIgnore]
        public int Id { get; set; }
        [Column("City")]  public string City { get; set;}
        [Column("Temp")] public float Temp { get; set;}
        [Column("Description")] public string Description { get; set;}
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] public DateTime Inserted { get; set; }
    }
}



