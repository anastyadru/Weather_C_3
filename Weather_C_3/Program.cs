using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Weather_C_3
{
    public class Program
    {
        private static async Task<WeatherData> GetWeatherDataAsync(string url)
        {
            using (var client = new HttpClient())
            {
                HttpResponseMessage response = await client.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    string responseBody = await response.Content.ReadAsStringAsync();
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);
                    return weatherData;
                }

                return null;
            }
        }

        private static string GetCityName()
        {
            string cityName;
            
            try
            {
                Console.WriteLine("Введите, для какого города прогноз погоды: Minsk, London, Paris, NewYork, Warsaw");
                cityName = Console.ReadLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Такого города не существует в списке. Введите город вручную: ");
                cityName = Console.ReadLine();
            }

            return cityName;
        }

        private static string GetWeatherType()
        {
            Console.WriteLine("На сколько дней Вы хотите знать прогноз погоды: на 1 день, на 5 дней?");
            string weatherType = Console.ReadLine();
            return weatherType;
        }

        private static void PrintCurrentWeather(WeatherData weatherData, string cityName)
        {
            var result = "";
            
            if (weatherData != null)
            {
                result += $"Прогноз погоды в городе {cityName} на сегодня: \n";
                result += $"Температура: {weatherData.Data.Temp}°C\n";
                result += $"Температура ощущается на: {weatherData.Data.FeelsLike}°C\n";
                result += $"Давление: {weatherData.Data.Pressure}Pa\n";
                result += $"Влажность: {weatherData.Data.Humidity}%\n";
            }
            else
            {
                result += $"Ошибка получения данных о погоде в городе {cityName}\n";
            }

            Console.WriteLine(result);
        }

        private static void PrintWeatherForecast(WeatherData weatherData, string cityName)
        {
            var result = "";
            
            if (weatherData != null)
            {
                result += $"Прогноз погоды в городе {cityName} на 5 дней: \n";
                for (int i = 0; i < weatherData.ForecastList.Count; i++)
                {
                    Forecast forecast = weatherData.ForecastList[i];
                    result += $"День {i + 1}: \n";
                    result += $"Дата: {forecast.Date}\n";
                    result += $"Температура: {forecast.Temp}°C\n";
                    result += $"Температура ощущается на: {forecast.FeelsLike}°C\n";
                    result += $"Давление: {forecast.Pressure}Pa\n";
                    result += $"Влажность: {forecast.Humidity}%\n";
                }
            }
            else
            {
                result += $"Ошибка получения данных о погоде в городе {cityName}\n";
            }

            Console.WriteLine(result);
        }

        private static async Task Main(string[] args)
        {
            string cityName = GetCityName();
            string weatherType = GetWeatherType();
            
            if (weatherType != null && weatherType.ToLower() == "на 1 день")
            {
                string url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
                WeatherData weatherData = await GetWeatherDataAsync(url);
                PrintCurrentWeather(weatherData, cityName);
            }
            else if (weatherType != null && weatherType.ToLower() == "на 5 дней")
            {
                string url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
                WeatherData weatherData = await GetWeatherDataAsync(url);
                PrintWeatherForecast(weatherData, cityName);
            }
            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, укажите, на сколько дней Вы хотите знать прогноз погоды: на 1 день или на 5 дней.");
            }

            Console.ReadLine();
        }
    }
}     