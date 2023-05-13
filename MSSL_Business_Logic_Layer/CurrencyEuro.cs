using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSSL_Business_Logic_Layer
{
    public class CurrencyEuro : ICurrency
    {
        public CurrencyEuro(string currencyCode, string currencyName, string noteSymbol, string coinSymbol)
        {
            CurrencyCode = currencyCode;
            CurrencyName = currencyName;
            NoteSymbol = noteSymbol;
            CoinSymbol = coinSymbol;
        }

        public CurrencyEuro()
        {
            CurrencyCode = "EUR";
            CurrencyName = "Euro";
            NoteSymbol = "E";
            CoinSymbol = "c";
        }
         
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string NoteSymbol { get; set; }
        public string CoinSymbol { get; set; }
    }
}
