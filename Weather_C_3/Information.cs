using System;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace Weather_C_3
{
    public class Information
    {
        
        public async Task<WeatherData> PrintAsync(string city)
        {
            using (var client = new HttpClient())
            {
                var url = $"https://api.openweathermap.org/data/2.5/weather?q={city}&appid=f1451f839fdb9f6c9c04a07f128795ec&units=metric&lang=ru";
                
                HttpResponseMessage response = await client.GetAsync(url);

                string responseBody = await response.Content.ReadAsStringAsync();
                
                WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                
                return weatherData;
            }
        }
        
    }
}