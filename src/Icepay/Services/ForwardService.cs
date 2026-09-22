using System.Net.Http;
using Icepay.Entities;
using Icepay.Infrastructure;
using Icepay.Requests;

namespace Icepay.Services;

public class ForwardService(ApiRequestor requestor)
{
    public Task<Forward> CreateAsync(string paymentKey, ForwardCreateOptions options, CancellationToken cancellationToken = default)
    {
        return requestor.SendAsync<Forward>(HttpMethod.Post, $"api/payments/{Uri.EscapeDataString(paymentKey)}/forward", options, cancellationToken);
    }
}