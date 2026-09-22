namespace ICEPAY.CheckoutSDK.Models.Response;

public class Refund: ICEPAY.CheckoutSDK.Models.Refund
{
    public Payment Payment { get; set; } = new();
}