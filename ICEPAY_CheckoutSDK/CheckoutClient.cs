using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ICEPAY.CheckoutSDK.Models;
using ICEPAY.CheckoutSDK.Models.Request;
using Checkout = ICEPAY.CheckoutSDK.Models.Response.Checkout;
using Forward = ICEPAY.CheckoutSDK.Models.Response.Forward;
using PaymentMethod = ICEPAY.CheckoutSDK.Models.Response.PaymentMethod;
using Refund = ICEPAY.CheckoutSDK.Models.Response.Refund;

namespace ICEPAY.CheckoutSDK;

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

    public Task<PaymentMethod[]?> GetPaymentMethods(CancellationToken cancellationToken = default)
    {
        return CallCheckoutApi<PaymentMethod[]?>(BaseUrl + "api/payments/methods", null, cancellationToken);
    }

    public Task<Checkout?> CreateCheckout(
        Models.Request.Checkout checkout, CancellationToken cancellationToken = default)
    {
        return CallCheckoutApi<Checkout>(BaseUrl + "api/payments", checkout, cancellationToken);
    }

    public Task<Refund?> Refund(Models.Request.Refund refund,
        string checkoutId, CancellationToken cancellationToken = default)
    {
        return CallCheckoutApi<Refund>(
            BaseUrl + "api/payments/" + checkoutId + "/refund", refund, cancellationToken);
    }

    public Task<Forward?> Forward(Models.Request.Forward forward,
        string checkoutId, CancellationToken cancellationToken = default)
    {
        return CallCheckoutApi<Forward>(
            BaseUrl + "api/payments/" + checkoutId + "/forward", forward, cancellationToken);
    }

    protected async Task<T?> CallCheckoutApi<T>(string url, object? body, CancellationToken cancellationToken)
    {
        var request = new HttpRequestMessage(body is null ? HttpMethod.Get : HttpMethod.Post, url);
        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonSerializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        request.Headers.Authorization = AuthHeader;

        var response = await HttpClient.SendAsync(request, cancellationToken);
        var responseJson = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            return JsonSerializer.Deserialize<T>(responseJson, JsonSerializerOptions);
        }

        if (!responseJson.StartsWith("{"))
        {
            response.EnsureSuccessStatusCode();
        }

        var apiError = JsonSerializer.Deserialize<Error>(responseJson, JsonSerializerOptions);
        throw new ApiException(apiError);
    }
}