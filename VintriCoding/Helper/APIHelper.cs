using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using VintriCodingAssignment.DTOs;
using Newtonsoft.Json;
using System.Configuration;

namespace VintriCodingAssignment.Helper
{
    public static class APIHelper
    {
        public static HttpClient ApiClient { get; set; }

        public static void Initialize()
        {
            ApiClient = new HttpClient
            {
                BaseAddress = new Uri(ConfigurationManager.AppSettings["baseAddress"])
            };
            ApiClient.DefaultRequestHeaders.Accept.Clear();
            ApiClient.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public static async Task<IEnumerable<BeerDTO>> GetBeerByName(String Name)
        {
            HttpResponseMessage response = await ApiClient.GetAsync("beers?beer_name=" + Name);
            IEnumerable<BeerDTO> FetchedBeers = null;
            if (response.IsSuccessStatusCode)
            {
                FetchedBeers = await response.Content.ReadAsAsync<IEnumerable<BeerDTO>>();
            } else
            {
                FetchedBeers = null;
            }

            return FetchedBeers;

        }

        public static async Task<Boolean> ValidateID(int id)
        {
            HttpResponseMessage response = await ApiClient.GetAsync("beers/" + id);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}