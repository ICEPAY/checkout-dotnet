namespace ICEPAY.Checkout;

public class Amount(int? value = null, string? currency = null)
{
    public int? Value { get; set; } = value;
    public string? Currency { get; set; } = currency;
}