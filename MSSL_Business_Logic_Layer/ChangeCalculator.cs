using Microsoft.Extensions.Logging;

namespace MSSL_Business_Logic_Layer
{
    public class ChangeCalculator : IChangeCalculator
    {
        private readonly ICurrency _currency;

        public ChangeCalculator(ICurrency currency) => _currency = currency;

        public TransactionChange CalculateChange(double amount, double purchasePrice)
        {
            var denominations = new double[] { 0.01, 0.02, 0.05, 0.1, 0.2, 0.5, 1, 2, 5, 10, 20, 50, 100, };
            var value = amount - purchasePrice;
            var breakdown =
                denominations
                    .OrderByDescending(x => x)
                    .Aggregate(new { value, denominations = new List<double>() },
                        (a, b) =>
                        {
                            var v = Math.Round(a.value, 2);
                            while (v >= b)
                            {
                                a.denominations.Add(b);
                                v -= b;
                            }
                            return new { value = v, a.denominations };
                        })
                    .denominations
                    .GroupBy(x => x)
                    .Select(x => new { denomination = x.Key, Count = x.Count() });

            TransactionChange transactionChange = new TransactionChange(_currency.CurrencyCode, _currency.CurrencyName, _currency.NoteSymbol, _currency.CoinSymbol)
            {
                CurrencyCode = _currency.CurrencyCode,
                CurrencyName = _currency.CurrencyName,
                NoteSymbol = _currency.NoteSymbol,
                CoinSymbol = _currency.CoinSymbol,
                DenominationCount = new List<DenominationCount>()
            };


            foreach (var element in breakdown)
            {
                transactionChange.DenominationCount.Add(new DenominationCount { Denomination = element.denomination, Count = element.Count });
            }

            return transactionChange;
        }
    }
}