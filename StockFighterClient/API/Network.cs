using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using StockFighterClient.Models;

namespace StockFighterClient.API {
    static class Network {

        private const String baseUrl = "https://api.stockfighter.io/ob/api/";

        public static async Task<JObject> GetAPIUp() {
            using (var cli = new HttpClient()) {
                buildClient(cli);

                HttpResponseMessage response = await cli.GetAsync("heartbeat");
                if (response.IsSuccessStatusCode) {
                    String res = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(res);
                } else {
                    Console.WriteLine("Error! Code: {0}\nTried url: {1}", response.StatusCode, response.RequestMessage.RequestUri.ToString());
                    return null;
                }
            }
        }

        public static async Task<JObject> GetVenueUp(String venue) {
            using (var cli = new HttpClient()) {
                buildClient(cli);

                String endpoint = String.Format("venues/{0}/heartbeat", venue);
                HttpResponseMessage response = await cli.GetAsync(endpoint);
                if (response.IsSuccessStatusCode) {
                    String res = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(res);
                } else {
                    Console.WriteLine("Error! Code: {0}\nTried url: {1}", response.StatusCode, response.RequestMessage.RequestUri.ToString());
                    return null;
                }
            }
        }

        public static async Task<Stock[]> GetVenueStocks(String venue) {
            using (var cli = new HttpClient()) {
                buildClient(cli);

                String endpoint = String.Format("venues/{0}/stocks", venue);
                HttpResponseMessage response = await cli.GetAsync(endpoint);
                if (response.IsSuccessStatusCode) {
                    String res = await response.Content.ReadAsStringAsync();
                    JArray arr = (JArray) JObject.Parse(res)["symbols"];
                    Stock[] stocks = new Stock[arr.Count];

                    for(int i = 0; i < arr.Count; i++) {
                        stocks[i] = new Stock{
                            Venue = venue,
                            Name = (String)arr[i]["name"],
                            Symbol = (String)arr[i]["symbol"]
                        };
                    }

                    return stocks;
                } else {
                    Console.WriteLine("Error! Code: {0}\nTried url: {1}", response.StatusCode, response.RequestMessage.RequestUri.ToString());
                    return null;
                }
            }
        }

        private static void buildClient(HttpClient cli) {
            cli.BaseAddress = new Uri(baseUrl);
            cli.DefaultRequestHeaders.Accept.Clear();
            cli.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
    }
}
