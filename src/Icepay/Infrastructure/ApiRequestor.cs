using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace Icepay.Infrastructure;

internal class ApiRequestor(IcepayClientOptions options)
{
    private static readonly HttpClient DefaultHttpClient = new();
    private readonly HttpClient httpClient = options.HttpClient ?? DefaultHttpClient;
    private readonly AuthenticationHeaderValue authHeader = new("Basic",
        Convert.ToBase64String(Encoding.UTF8.GetBytes($"{options.MerchantId}:{options.MerchantSecret}")));

    public async Task<T> SendAsync<T>(HttpMethod method, string path, object? body, CancellationToken cancellationToken)
    {
        using var request = new HttpRequestMessage(method, options.ApiBase + path) { Headers = { Authorization = authHeader } };

        if (body is not null)
        {
            request.Content = new StringContent(JsonSerializer.Serialize(body, JsonSerialization.Options), Encoding.UTF8, "application/json");
        }

        using var response = await httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
        {
            throw new IcepayException(response.StatusCode, json);
        }

        return JsonSerializer.Deserialize<T>(json, JsonSerialization.Options)!;
    }
}