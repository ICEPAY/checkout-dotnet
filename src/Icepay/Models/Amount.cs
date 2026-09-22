namespace ICEPAY.CheckoutSDK.Models;

public class Amount(int? value = null, string? currency = null)
{
    public const string CurrencyEur = "eur";
    public const string CurrencyBgp = "gbp";
    public const string CurrencyUsd = "usd";
    public const string CurrencyPln = "pln";
    public const string CurrencySek = "sek";
    public const string CurrencyNok = "nok";
    public const string CurrencyDkk = "dkk";
    public const string CurrencyCzk = "czk";
    
    public int? Value { get; set; } = value;
    public string? Currency { get; set; } = currency;
}