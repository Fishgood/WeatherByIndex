using System;
using System.Threading.Tasks;

namespace WeatherApp
{
    public class Core
    {
        public static async Task<Weather> GetWeather(string cityName)
        {
            string key = "088bf18e1e0dff6e36e16733f079d626";
            string queryString = "http://api.openweathermap.org/data/2.5/weather?q="
                + cityName + "&units=metric&appid=" + key;

            var results = await DataService.getDataFromService(queryString).ConfigureAwait(false);

            if (results["weather"] != null)
            {
                Weather weather = new Weather();
                weather.Title = (string)results["name"];
                //double F = (double)results["main"]["temp"];
                //double C = (F - 32) / 1.8;
                //string Temp = System.Convert.ToString(C);
                weather.Temperature = (string)results["main"]["temp"] + " C";
                double mph = (double)results["wind"]["speed"];
                double km = mph * 1.609344;
                string convert = System.Convert.ToString(km); 
                weather.Wind = (string)convert + " km";
                weather.Humidity = (string)results["main"]["humidity"] + " %";
                weather.Visibility = (string)results["weather"][0]["main"];

                DateTime time = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
                DateTime sunrise = time.AddSeconds((double)results["sys"]["sunrise"]);
                DateTime sunset = time.AddSeconds((double)results["sys"]["sunset"]);
                weather.Sunrise = sunrise.ToString() + " UTC";
                weather.Sunset = sunset.ToString() + " UTC";
                return weather;
            }
            else
            {
                return null;
            }
        }
    }
}