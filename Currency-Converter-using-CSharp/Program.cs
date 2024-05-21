using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Currency_Converter_using_CSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            CurrencyConverter converter = new CurrencyConverter();

            Console.WriteLine("Do you want to see all available currencies? (yes/no)");
            string showCurrencies = Console.ReadLine().ToLower();

            while (showCurrencies != "yes" && showCurrencies != "no")
            {
                Console.WriteLine("Invalid input. Please enter 'yes' or 'no':");
                showCurrencies = Console.ReadLine().ToLower();
            }

            if (showCurrencies == "yes")
            {
                converter.DisplayAllCurrencies();
            }

            double amount = GetValidDouble("Enter amount to convert:");

            string fromCurrency = GetValidCurrency(converter, "Enter currency to convert from (e.g., USD, EUR, GBP):");
            string toCurrency = GetValidCurrency(converter, "Enter currency to convert to (e.g., USD, EUR, GBP):");

            double convertedAmount = converter.ConvertCurrency(amount, fromCurrency, toCurrency);
            Console.WriteLine($"Converted amount: {convertedAmount} {toCurrency}");

            Console.WriteLine();
            Console.WriteLine("Thank You");
            Console.WriteLine("-By Akshay");
        }

        static double GetValidDouble(string prompt)
        {
            double result;
            Console.WriteLine(prompt);

            while (!double.TryParse(Console.ReadLine(), out result) || result <= 0)
            {
                Console.WriteLine("Invalid input. Please enter a positive number:");
            }

            return result;
        }

        static string GetValidCurrency(CurrencyConverter converter, string prompt)
        {
            string currency;
            Console.WriteLine(prompt);

            while (true)
            {
                currency = Console.ReadLine().ToUpper();
                if (converter.IsValidCurrency(currency))
                {
                    break;
                }
                Console.WriteLine("Invalid currency. Please enter a valid currency code:");
            }

            return currency;
        }
    }
}
