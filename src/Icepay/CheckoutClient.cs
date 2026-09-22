using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Icepay.Entities;
using Icepay.Services;

namespace Icepay;

public class CheckoutClient(HttpMessageInvoker? httpClient = null, JsonSerializerOptions? jsonSerializerOptions = null)
{
    protected const string BaseUrl = "https://checkout.icepay.com/";

    protected readonly HttpMessageInvoker HttpClient = httpClient ?? new HttpClient();

    protected readonly JsonSerializerOptions JsonSerializerOptions = jsonSerializerOptions ?? new()
    {
        PropertyNameCaseInsensitive = true,
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
        Converters = { new JsonStringEnumConverter() }
    };

    protected AuthenticationHeaderValue? AuthHeader;

    public CheckoutClient WithAuthorization(string merchantId, string merchantSecret)
    {
        var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{merchantId}:{merchantSecret}"));
        AuthHeader =
            new AuthenticationHeaderValue("Basic", authToken);

        return this;
    }

    public Task<PaymentMethod[]?> GetPaymentMethods()
    {
        return CallCheckoutApi<PaymentMethod[]?>(BaseUrl + "api/payments/methods", null);
    }

    public Task<Payment?> CreateCheckout(PaymentCreateOptions options)
    {
        return CallCheckoutApi<Payment>(BaseUrl + "api/payments", options);
    }

    public Task<Refund?> Refund(RefundCreateOptions refund, string checkoutId)
    {
        return CallCheckoutApi<Refund>(
            BaseUrl + "api/payments/" + Uri.EscapeDataString(checkoutId) + "/refund", refund);
    }

    public Task<Forward?> Forward(ForwardCreateOptions forward, string checkoutId)
    {
        return CallCheckoutApi<Forward>(
            BaseUrl + "api/payments/" + Uri.EscapeDataString(checkoutId) + "/forward", forward);
    }

    protected async Task<T?> CallCheckoutApi<T>(string url, object? body)
    {
        using var request = new HttpRequestMessage(body is null ? HttpMethod.Get : HttpMethod.Post, url);
        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonSerializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        request.Headers.Authorization = AuthHeader;

        using var response = await HttpClient.SendAsync(request, CancellationToken.None);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
            throw new HttpRequestException($"ICEPAY API returned {(int)response.StatusCode}: {responseJson}");

        return JsonSerializer.Deserialize<T>(responseJson, JsonSerializerOptions);
    }
}