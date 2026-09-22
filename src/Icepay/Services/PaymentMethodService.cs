using System.Net.Http;
using Icepay.Entities;
using Icepay.Infrastructure;

namespace Icepay.Services;

public class PaymentMethodService(ApiRequestor requestor)
{
    public Task<PaymentMethod[]> ListAsync(CancellationToken cancellationToken = default)
    {
        return requestor.SendAsync<PaymentMethod[]>(HttpMethod.Get, "api/payments/methods", null, cancellationToken);
    }
}