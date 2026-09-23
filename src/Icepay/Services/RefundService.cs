namespace Icepay.Services;

public class RefundService
{
    private readonly ApiRequestor requestor;
    internal RefundService(ApiRequestor requestor) => this.requestor = requestor;

    public Task<Refund> CreateAsync(string paymentKey, RefundCreateOptions options, CancellationToken cancellationToken = default)
        => requestor.SendAsync<Refund>(HttpMethod.Post, $"api/payments/{Uri.EscapeDataString(paymentKey)}/refund", options, cancellationToken);
}