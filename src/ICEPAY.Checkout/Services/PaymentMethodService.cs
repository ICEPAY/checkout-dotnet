namespace ICEPAY.Checkout;

public class PaymentMethodService
{
    private readonly ApiRequestor requestor;
    internal PaymentMethodService(ApiRequestor requestor) => this.requestor = requestor;

    public Task<PaymentMethod[]> ListAsync(CancellationToken cancellationToken = default)
        => requestor.SendAsync<PaymentMethod[]>(HttpMethod.Get, "api/payments/methods", null, cancellationToken);
}