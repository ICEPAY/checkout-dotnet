namespace ICEPAY.CheckoutSDK.Models.Response;

public class Checkout : Payment
{
    public Refund[]? Refunds;
    public Forward[]? Forwards;
}