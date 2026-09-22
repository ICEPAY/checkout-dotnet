using System.Net.Http;
using Icepay.Entities;
using Icepay.Infrastructure;
using Icepay.Requests;

namespace Icepay.Services;

public class RefundService(ApiRequestor requestor)
{
    public Task<Refund> CreateAsync(string paymentKey, RefundCreateOptions options, CancellationToken cancellationToken = default)
    {
        return requestor.SendAsync<Refund>(HttpMethod.Post, $"api/payments/{Uri.EscapeDataString(paymentKey)}/refund", options, cancellationToken);
    }
}