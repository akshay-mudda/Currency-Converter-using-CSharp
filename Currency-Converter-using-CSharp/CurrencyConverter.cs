using System;
using System.Collections.Generic;
using System.Net;
using System.Configuration;
using Newtonsoft.Json;
using System.Linq;

namespace Currency_Converter_using_CSharp
{
    public class CurrencyConverter
    {
        private readonly string ApiUrl;
        private Dictionary<string, double> exchangeRates;

        public CurrencyConverter()
        {
            ApiUrl = ConfigurationManager.AppSettings["ApiUrl"];
            exchangeRates = FetchExchangeRates();
        }

        private Dictionary<string, double> FetchExchangeRates()
        {
            using (WebClient web = new WebClient())
            {
                try
                {
                    string json = web.DownloadString(ApiUrl);
                    var data = JsonConvert.DeserializeObject<dynamic>(json);
                    return JsonConvert.DeserializeObject<Dictionary<string, double>>(data["rates"].ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error fetching exchange rates: " + ex.Message);
                    return null;
                }
            }
        }

        public double ConvertCurrency(double amount, string fromCurrency, string toCurrency)
        {
            if (exchangeRates == null || !exchangeRates.ContainsKey(fromCurrency) || !exchangeRates.ContainsKey(toCurrency))
            {
                Console.WriteLine("Exchange rates not available or invalid currencies.");
                return 0;
            }

            double fromRate = exchangeRates[fromCurrency];
            double toRate = exchangeRates[toCurrency];

            return (amount / fromRate) * toRate;
        }

        public void DisplayAllCurrencies()
        {
            if (exchangeRates == null)
            {
                Console.WriteLine("Exchange rates not available.");
                return;
            }

            Console.WriteLine("Available Currencies:");

            // Determine the maximum length of currency code
            int maxLength = exchangeRates.Keys.Max(currency => currency.Length);

            // Determine the number of columns
            int numColumns = 18;
            int numRows = (int)Math.Ceiling((double)exchangeRates.Count / numColumns);

            for (int i = 0; i < numRows; i++)
            {
                for (int j = 0; j < numColumns; j++)
                {
                    int index = i + j * numRows;
                    if (index >= exchangeRates.Count)
                        break;

                    string currency = exchangeRates.Keys.ElementAt(index);
                    int padding = maxLength - currency.Length;

                    // Add padding to align currencies
                    Console.Write($"{currency}{new string(' ', padding + 2)}");
                }
                Console.WriteLine();
            }
        }

        public bool IsValidCurrency(string currency)
        {
            return exchangeRates != null && exchangeRates.ContainsKey(currency);
        }
    }
}
