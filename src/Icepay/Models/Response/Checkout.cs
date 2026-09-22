namespace ICEPAY.CheckoutSDK.Models.Response;

public class Checkout : Payment
{
    public Refund[]? Refunds { get; set; }
    public Forward[]? Forwards { get; set; }
}