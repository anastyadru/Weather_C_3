using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Weather_C_3
{
    public class WeatherData
    {
        public Coordinates Coord { get; set; }
        
        public List<Weather> Weather { get; set; }
        
        [JsonProperty("base")]
        
        public string Base { get; set; }
        
        public Data Data { get; set; }
        
        public List<Forecast> ForecastList { get; set; }
        
        public int Visibility { get; set; }
        
        public Wind Wind { get; set; }
        
        public Clouds Clouds { get; set; }
        
        public int Dt { get; set; }
        
        public Sys Sys { get; set; }
        
        public int Id { get; set; }
        
        public string Name { get; set; }
        
        public int Cod { get; set; }
        
        public string City { get; set; }
    }
}