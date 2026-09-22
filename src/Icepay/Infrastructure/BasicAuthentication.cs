using System.Net.Http.Headers;
using System.Text;

namespace Icepay.Infrastructure;

internal static class BasicAuthentication
{
    internal static AuthenticationHeaderValue Create(string merchantId, string merchantSecret)
    {
        var token = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{merchantId}:{merchantSecret}"));

        return new AuthenticationHeaderValue("Basic", token);
    }
}