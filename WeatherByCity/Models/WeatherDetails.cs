using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherByCity.Models
{
    public class WeatherDetails
    {
        public string City { get; set; }
        public string Country { get; set; }
        public double Temprature { get; set; }
        public double TempratureMax { get; set; }
        public double TempratureMin { get; set; }
        public double Humidity { get; set; }
        public string MainWeather { get; set; }
        public string Description { get; set; }
        public double Pressure { get; set; }               
    }
}