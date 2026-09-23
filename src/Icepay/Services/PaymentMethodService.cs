namespace Icepay.Services;

public class PaymentMethodService(ApiRequestor requestor)
{
    public Task<PaymentMethod[]> ListAsync(CancellationToken cancellationToken = default)
        => requestor.SendAsync<PaymentMethod[]>(HttpMethod.Get, "api/payments/methods", null, cancellationToken);
}