namespace ICEPAY.Checkout;

public class IcepayClientOptions
{
    public string MerchantId { get; set; } = string.Empty;
    public string MerchantSecret { get; set; } = string.Empty;
    public string ApiBase { get; set; } = "https://checkout.icepay.com/";
    public HttpClient? HttpClient { get; set; }
}