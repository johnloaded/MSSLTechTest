// See https://aka.ms/new-console-template for more information

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MSSL_Business_Logic_Layer;
using System.Diagnostics;

IHost host = Host.CreateDefaultBuilder().ConfigureServices(
        services =>
        {
            services.AddSingleton<IChangeCalculator, ChangeCalculator>();
            services.AddSingleton<ICurrency, CurrencySterling>(); //services.AddSingleton<ICurrency, CurrencyEuro>(); - swap in to use Euros
        })
    .Build();

var app = host.Services.GetRequiredService<IChangeCalculator>();

string? response;
do
{
    Console.WriteLine("Enter amount of money given, type X to exit");
    response = Console.ReadLine()?.ToUpper();
    if (double.TryParse(response, out var amount))
    {
        Console.WriteLine("Enter purchase price, type X to exit");
        response = Console.ReadLine()?.ToUpper();
        if (double.TryParse(response, out var purchasePrice))
        {
            if (response != "X")
            {
                var transactionChange = app.CalculateChange(amount, purchasePrice);

                Console.WriteLine("Your change is :");
                foreach (var denominationCount in transactionChange.DenominationCount)
                {
                    Console.WriteLine(denominationCount.Denomination >= 1
                        ? $"{denominationCount.Count} x {transactionChange.NoteSymbol}{denominationCount.Denomination}"
                        : $"{denominationCount.Count} x {denominationCount.Denomination * 100}{transactionChange.CoinSymbol}");
                }

                Console.WriteLine();
            }
        }
        else if (response != "X")
        {
            Console.WriteLine("Please enter a valid value");
        }
    }
    else if (response != "X")
    {
        Console.WriteLine("Please enter a valid value");
    }
} while (response != "X");





