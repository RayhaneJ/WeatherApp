using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.ViewModels.Helpers
{
    public class AccuWeatherHelper
    {
        public const string BaseUrl = "http://dataservice.accuweather.com/";
        public const string AutocompleteEndpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}";
        public const string CurrentConditionsEndpoint = "currentconditions/v1/{0}?apikey={1}";
        public const string ApiKey = "yLmlH114d2lH29DniqGTAXKKd1Jicavp";

        public static async Task<List<City>> GetCities(string query)
        {
            var cities = new List<City>();

            string url = BaseUrl + string.Format(AutocompleteEndpoint, ApiKey, query);

            using(HttpClient httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await httpClient.SendAsync(request);

                var jsonResponse = await response.Content.ReadAsStringAsync();

                cities = JsonConvert.DeserializeObject<List<City>>(jsonResponse);
            }

            return cities;
        }

        public static async Task<CurrentConditions> GetCurrentConditions(string cityKey)
        {
            CurrentConditions currentConditions = new CurrentConditions();
            string url = BaseUrl + string.Format(CurrentConditionsEndpoint, cityKey, ApiKey);

            using(HttpClient httpClient = new HttpClient())
            {
                var request = new HttpRequestMessage(HttpMethod.Get, url);

                var response = await httpClient.SendAsync(request);
                var jsonResponse = await response.Content.ReadAsStringAsync();

                currentConditions = JsonConvert.DeserializeObject<List<CurrentConditions>>(jsonResponse).FirstOrDefault();
            }

            return currentConditions;
        }
    }
}
