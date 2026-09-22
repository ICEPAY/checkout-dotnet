using System.Net.Http;
using Icepay.Entities;
using Icepay.Infrastructure;
using Icepay.Requests;

namespace Icepay.Services;

public class PaymentService(ApiRequestor requestor)
{
    public Task<Payment> CreateAsync(PaymentCreateOptions options, CancellationToken cancellationToken = default)
    {
        return requestor.SendAsync<Payment>(HttpMethod.Post, "api/payments", options, cancellationToken);
    }

    public Task<Payment> GetAsync(string paymentKey, CancellationToken cancellationToken = default)
    {
        return requestor.SendAsync<Payment>(HttpMethod.Get, $"api/payments/{Uri.EscapeDataString(paymentKey)}", null, cancellationToken);
    }
}