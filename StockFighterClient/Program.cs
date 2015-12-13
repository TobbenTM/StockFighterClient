using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockFighterClient.API;
using StockFighterClient.Models;
using Newtonsoft.Json.Linq;

namespace StockFighterClient {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("Checking general API");

            Task<JObject> status = Network.GetAPIUp();
            status.Wait();

            if(status.Result != null)
                Console.WriteLine(status.Result.ToString());

            Console.WriteLine("Checking venue TESTEX");

            Task<JObject> venueStatus = Network.GetVenueUp("TESTEX");
            venueStatus.Wait();

            if (venueStatus.Result != null)
                Console.WriteLine(venueStatus.Result.ToString());

            Console.WriteLine("Stocks on venue TESTEX:\n");

            Task<Stock[]> venueStocks = Network.GetVenueStocks("TESTEX");
            venueStocks.Wait();

            if (venueStocks.Result != null) {
                foreach(Stock stock in venueStocks.Result) {
                    Console.WriteLine(stock.ToString());
                }
            }

            Console.ReadLine();
        }
    }
}
