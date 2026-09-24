namespace ICEPAY.Checkout;

public class PaymentService
{
    private readonly ApiRequestor requestor;
    internal PaymentService(ApiRequestor requestor) => this.requestor = requestor;

    public Task<Payment> CreateCheckoutAsync(CheckoutCreateOptions options, CancellationToken cancellationToken = default)
        => requestor.SendAsync<Payment>(HttpMethod.Post, "api/payments", options, cancellationToken);

    public Task<Payment> GetAsync(string paymentKey, CancellationToken cancellationToken = default)
        => requestor.SendAsync<Payment>(HttpMethod.Get, $"api/payments/{Uri.EscapeDataString(paymentKey)}", null, cancellationToken);
}