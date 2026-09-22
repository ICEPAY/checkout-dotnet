using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Icepay.Infrastructure;

public class ApiRequestor(IcepayClientOptions options, HttpClient httpClient)
{
    private readonly AuthenticationHeaderValue authHeader =
        BasicAuthentication.Create(options.MerchantId, options.MerchantSecret);

    public async Task<T> SendAsync<T>(HttpMethod method, string path, object? body, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, options.ApiBase + path) { Headers = { Authorization = authHeader } };

        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonSerialization.Options);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new IcepayException(response.StatusCode, responseJson);
        }

        return JsonSerializer.Deserialize<T>(responseJson, JsonSerialization.Options)!;
    }
}