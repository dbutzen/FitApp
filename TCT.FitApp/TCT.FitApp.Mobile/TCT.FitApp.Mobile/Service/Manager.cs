using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace TCT.FitApp.Mobile.Service
{
    public static class Manager
    {
        private static string GetBaseUri()
        {
            return "https://nutritionix-api.p.rapidapi.com/v1_1/";
        }
        public static async Task<Product> GetByUPC(string upc)
        {
            try
            {
                string result;
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{GetBaseUri()}item?upc={upc}"),
                    Headers =
                            {
                                { "x-rapidapi-key", "35be3e7930msh6d601bfd8008df4p136ce8jsna6728d5864d6" },
                                { "x-rapidapi-host", "nutritionix-api.p.rapidapi.com" },
                            }
                };
                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();
                }
                var item = JsonConvert.DeserializeObject<Product>(result);
                return item;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public static async Task<List<Product>> GetByName(string name)
        {
            try
            {
                var items = new List<Product>();
                string result;
                var client = new HttpClient();
                var request = new HttpRequestMessage
                {
                    Method = HttpMethod.Get,
                    RequestUri = new Uri($"{GetBaseUri()}search/{name.Replace(" ","%20").Replace(",", "%20")}?fields=item_name%2Citem_description%2Citem_id%2Cbrand_name%2Cnf_calories%2Cnf_protein"),
                    Headers =
                            {
                                { "x-rapidapi-key", "35be3e7930msh6d601bfd8008df4p136ce8jsna6728d5864d6" },
                                { "x-rapidapi-host", "nutritionix-api.p.rapidapi.com" },
                            }
                };

                using (var response = await client.SendAsync(request))
                {
                    response.EnsureSuccessStatusCode();
                    result = await response.Content.ReadAsStringAsync();
                }
                var reponse = JsonConvert.DeserializeObject<Response>(result);

                foreach (var h in reponse.hits)
                {
                    items.Add(h.fields);
                }
                return items;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
