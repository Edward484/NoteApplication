using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteApplication.Model;
using NoteApplication.ViewModel.Commands;
using NoteApplication.ViewModel.Helpers;

namespace NoteApplication.ViewModel.Helpers
{
    class AccuWeatherHelper
    {
        static string apiKey = "AHCvs6uGxYcR7dviOuIQbc9hpiwDjG6E";
            //"AHCvs6uGxYcR7dviOuIQbc9hpiwDjG6E";
        static string baseURL = "http://dataservice.accuweather.com/";
        static string locAutoComplete = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        static string currentConditions = "currentconditions/v1/{0}?apikey={1}";
        static string getCity = "/locations/v1/{0}?apikey={1}";

        public async static Task<List<City>> GetCities(string query)
        {
            List<City> cities = new();

            string url = baseURL + string.Format(locAutoComplete, apiKey, query);
            using(HttpClient client = new())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                cities = JsonConvert.DeserializeObject<List<City>>(json);
            }

            return cities;
        }
        
        public async static Task<CurrentWeather> GetCurrentWeatherA(string cityKey)
        {
            CurrentWeather currentWeather = new();

            if (DesignerProperties.GetIsInDesignMode(new System.Windows.DependencyObject()) == false)
            {

                string url = baseURL + string.Format(currentConditions, cityKey, apiKey);
                using (HttpClient client = new())
                {
                    var response = await client.GetAsync(url);
                    string json = await response.Content.ReadAsStringAsync();
                    currentWeather = (JsonConvert.DeserializeObject<List<CurrentWeather>>(json)).First();
                }
            }
                return currentWeather;
            
        }

        public static async Task<City>GetCityAsync(string cityId)
        {
            City city = new();
            string url = baseURL + string.Format(getCity, cityId,apiKey);

            using (HttpClient client = new())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();
                city = JsonConvert.DeserializeObject<City>(json);
            }

            return city;
        }
    }
}
