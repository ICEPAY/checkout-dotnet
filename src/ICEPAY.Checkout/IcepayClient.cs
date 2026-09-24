namespace ICEPAY.Checkout;

public class IcepayClient
{
    public IcepayClient(string merchantId, string merchantSecret)
        : this(new IcepayClientOptions { MerchantId = merchantId, MerchantSecret = merchantSecret })
    {
    }

    public IcepayClient(IcepayClientOptions options)
    {
        if (string.IsNullOrWhiteSpace(options.MerchantId))
            throw new ArgumentException("MerchantId is required.", nameof(options));
        if (string.IsNullOrWhiteSpace(options.MerchantSecret))
            throw new ArgumentException("MerchantSecret is required.", nameof(options));

        var requestor = new ApiRequestor(options);

        Payments = new PaymentService(requestor);
        Refunds = new RefundService(requestor);
        Forwards = new ForwardService(requestor);
        PaymentMethods = new PaymentMethodService(requestor);
    }

    public PaymentService Payments { get; }
    public RefundService Refunds { get; }
    public ForwardService Forwards { get; }
    public PaymentMethodService PaymentMethods { get; }
}