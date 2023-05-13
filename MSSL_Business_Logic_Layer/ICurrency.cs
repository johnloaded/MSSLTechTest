namespace MSSL_Business_Logic_Layer;

public interface ICurrency
{
    string CurrencyCode { get; }
    string CurrencyName { get; }
    string NoteSymbol { get;}
    string CoinSymbol { get; }
}