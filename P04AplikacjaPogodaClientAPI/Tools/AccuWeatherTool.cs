using Newtonsoft.Json;
using P04AplikacjaPogodaClientAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04AplikacjaPogodaClientAPI.Tools
{
    public class AccuWeatherTool
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplate_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language=pl-PL";
        private const string api_key = "";
        public async Task<City[]> GetLocation(string locationName)
        {
            string url = base_url + "/" + string.Format(autocomplate_endpoint, api_key, locationName);

            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(url);
                string json = await response.Content.ReadAsStringAsync();

                City[] cities = JsonConvert.DeserializeObject<City[]>(json);

                return cities;
            }
        }
    }
}
