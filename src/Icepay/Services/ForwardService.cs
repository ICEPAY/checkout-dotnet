namespace Icepay.Services;

public class ForwardService(ApiRequestor requestor)
{
    public Task<Forward> CreateAsync(string paymentKey, ForwardCreateOptions options, CancellationToken cancellationToken = default)
        => requestor.SendAsync<Forward>(HttpMethod.Post, $"api/payments/{Uri.EscapeDataString(paymentKey)}/forward", options, cancellationToken);
}