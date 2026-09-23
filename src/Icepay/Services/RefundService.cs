namespace Icepay.Services;

public class RefundService(ApiRequestor requestor)
{
    public Task<Refund> CreateAsync(string paymentKey, RefundCreateOptions options, CancellationToken cancellationToken = default)
        => requestor.SendAsync<Refund>(HttpMethod.Post, $"api/payments/{Uri.EscapeDataString(paymentKey)}/refund", options, cancellationToken);
}