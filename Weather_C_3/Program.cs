using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Http;

namespace Weather_C_3
{
    public class Program
    {

        static async Task Main(string[] args)
        {
            string url;
            string cityName;

            try
            {
                Console.WriteLine("Введите, для какого города прогноз погоды: Minsk, London, Paris, NewYork, Warsaw");

                cityName = Console.ReadLine();
                url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Console.WriteLine("Такого города не существует в списке. Введите город вручную: ");

                cityName = Console.ReadLine();
                url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
            }

            string weatherType;
            Console.WriteLine("На сколько дней Вы хотите знать прогноз погоды: на 1 день, на 5 дней?");
            weatherType = Console.ReadLine();

            if (weatherType.ToLower() == "На 1 день")
            {
                url = $"https://api.openweathermap.org/data/2.5/weather?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
            }

            else if (weatherType.ToLower() == "На 5 дней")
            {
                url = $"https://api.openweathermap.org/data/2.5/forecast?q={cityName}&appid=d6bfd60ae10dc578300a860f105ed749&units=metric&lang=ru";
            }

            else
            {
                Console.WriteLine("Некорректный ввод. Пожалуйста, укажите, на сколько дней Вы хотите знать прогноз погоды: на день или на 5 дней?");
                return;
            }

            var information = new Information();
            WeatherData weatherData = await information.PrintAsync(url);

            var result = "";

            if (weatherData != null)
            {
                result += $"Погода в городе {cityName}: \n";
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
            Console.ReadLine();
        }

        private class Information
        {

            public async Task<WeatherData> PrintAsync(string city)
            {
                using (var client = new HttpClient())
                {
                    HttpResponseMessage response = await client.GetAsync(city);

                    string responseBody = await response.Content.ReadAsStringAsync();

                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(responseBody);

                    return weatherData;
                }
            }

        }

    }
}