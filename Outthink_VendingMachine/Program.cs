using Flurl;
using Flurl.Http;
using Outthink_VendingMachine_API.Commands;
using Outthink_VendingMachine_API.DTOs;
using Outthink_VendingMachine_API.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Outthink_VendingMachine
{
    internal class Program
    {
        const string API_HOST = "https://localhost:44322";

        public static void StartVMachine()
        {   
            //Start vending machine
            Console.Out.WriteLine("Welcome to Outthink vending machine!. \n\nPlease, before continue start Web API project and press any key.");
            Console.ReadKey();

            Console.Out.WriteLine("\nAvailable products:\n");

            // Get products from API
            var products = API_HOST.AppendPathSegment("products").GetJsonAsync<List<ProductDTO>>().Result;

            Console.Out.WriteLine(string.Join('\n', products.Select(p => p.ToString())));

            Console.Out.WriteLine($"\n\nPlease introduce the coins to pay [0.1, 0.2, 0.5, 1].\n Press 'ENTER' after each coin and type 'END' to finish.\n");

            // To temporally persist user coins during purchase
            List<double> insertedCoins = new List<double>();
            bool end = false;

            while (!end)
            {
                var line = Console.ReadLine();
                if (line == "END")
                {
                    end = true;
                } 
                else
                {
                    var coinValue = double.Parse(line, CultureInfo.InvariantCulture);
                    insertedCoins.Add(coinValue);
                    var result = API_HOST.AppendPathSegment("coins").PostJsonAsync(new AddCoinCommand { Value = coinValue }).ReceiveString().Result;
                    if (!bool.Parse(result)) { Console.Out.WriteLine("Coin not supported. Please, try again."); }
                }
            }

            Console.Out.WriteLine("\nPlease, select the product to buy (typing its ID from previous list). Or type 'RETURN' to request your coins.");
            var productId = Console.ReadLine();

            var returnedChange = new List<CoinDTO>();
            if (productId == "RETURN")
            {
                returnedChange = API_HOST.AppendPathSegment("products-purchase-cancellation").PostJsonAsync(new CancelPurchaseProductCommand {  InsertedCoins = insertedCoins }).ReceiveJson<List<CoinDTO>>().Result;
            } 
            else
            {
                var purchaseResult = API_HOST.AppendPathSegment("products-purchase").PostJsonAsync(new PurchaseProductCommand { ProductId = int.Parse(productId), InsertedCoins = insertedCoins }).ReceiveJson<ProductPurchaseResult>().Result;
                switch (purchaseResult.InfoMsg)
                {
                    case "Thank you":
                        Console.Out.WriteLine($"\nThank you. Here is your {products.FirstOrDefault(p => p.Id == int.Parse(productId))?.Name}.");
                        returnedChange = purchaseResult.Change;
                        break;
                    default:
                        Console.Out.WriteLine(purchaseResult.InfoMsg);
                        // Proceed to return the money to restart vending machine
                        returnedChange = API_HOST.AppendPathSegment("products-purchase-cancellation").PostJsonAsync(new CancelPurchaseProductCommand { InsertedCoins = insertedCoins }).ReceiveJson<List<CoinDTO>>().Result;
                        break;
                }
            }
            
            Console.Out.WriteLine($"{(returnedChange.Count > 0 ? "\nHere is your change:" : "")} \n {string.Join(", ", returnedChange.Select(p => p.ToString()))}");

            Console.Out.WriteLine("\nDo you want to buy another product? (Y/N)");
            var decision = Console.ReadLine();
            if (decision.ToLower() == "y")
            {
                StartVMachine();
            }
        }

        static void Main(string[] args)
        {
            StartVMachine();
        }
    }
}
