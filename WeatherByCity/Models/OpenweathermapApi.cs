using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;

namespace WeatherByCity.Models
{
    class OpenweathermapApi
    {
        private string code { get; set; }
        private string info { get; set; }
        private string message { get; set; }

        public WeatherDetails Get_Details_By_City(string city)
        {
            AppSettingsReader webConfig = new AppSettingsReader();
            var appKey = webConfig.GetValue("appKey", "".GetType()).ToString();

            Uri url = new Uri(string.Format("https://api.openweathermap.org/data/2.5/weather?q=" + city+"&appid="+ appKey + ""));
            var request = HttpWebRequest.Create(url);
            
            request.ContentType = "application/json";
            request.Method = "GET";
            request.Headers.Add("Authorization", appKey);

            try
            {
                WeatherDetails weatherDetails = new WeatherDetails();

                var response = (HttpWebResponse)request.GetResponse();
                var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();

                if (responseString != null)
                {
                    OpenWeathermapApiResponse apiResponse = new OpenWeathermapApiResponse();

                    apiResponse = JsonConvert.DeserializeObject<OpenWeathermapApiResponse>(responseString);

                    weatherDetails.Country = apiResponse.sys.country.ToString();
                    weatherDetails.City = apiResponse.name.ToString();
                    weatherDetails.Temprature = (double) Math.Round(apiResponse.main.temp - 273.15,1); // Convert Kelvin to Celcius
                    weatherDetails.TempratureMax = (double) Math.Round(apiResponse.main.temp_max - 273.15);
                    weatherDetails.TempratureMin = (double) Math.Round(apiResponse.main.temp_min - 273.15);
                    weatherDetails.Humidity = (double)apiResponse.main.humidity;
                    weatherDetails.Pressure = (double)apiResponse.main.pressure;
                    for(int i = 0; i < apiResponse.weather.Count ;i++)
                    {
                        weatherDetails.MainWeather = apiResponse.weather[i].main.ToString();
                        weatherDetails.Description = apiResponse.weather[i].description.ToString();
                    }                  
                    return weatherDetails;
                }
                return null;
            }
            catch(WebException ex)
            {
                return null;
            }
        }      
    }
}