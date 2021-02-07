using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WeatherByCity.Models;

namespace WeatherByCity.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
           return View();
        }

        [HttpGet]
        public ActionResult GetWeather(string city)
        {            
            if (!string.IsNullOrEmpty(city))
            {
                WeatherDetails weatherDetails = new WeatherDetails();

                OpenweathermapApi weatherDetailsApi = new OpenweathermapApi();

                weatherDetails = weatherDetailsApi.Get_Details_By_City(city);
                if(weatherDetails != null)
                {
                    return View(weatherDetails);
                }
                else
                {
                    ViewBag.Message = "City does not exist!";
                    return View("Index");                   
                }                
            }
            else
            {
                return View("Index");
            }
        }      


        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}