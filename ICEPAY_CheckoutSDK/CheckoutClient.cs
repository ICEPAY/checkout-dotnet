using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using ICEPAY.CheckoutSDK.Models.Response;
using PaymentMethod = ICEPAY.CheckoutSDK.Models.Response.PaymentMethod;

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

    public Task<PaymentMethod[]?> GetPaymentMethods()
    {
        return CallCheckoutApi<PaymentMethod[]?>(BaseUrl + "api/payments/methods", null);
    }

    public Task<Checkout?> CreateCheckout(
        Models.Request.Checkout checkout)
    {
        return CallCheckoutApi<Checkout>(BaseUrl + "api/payments", checkout);
    }

    public Task<Refund?> Refund(Models.Request.Refund refund,
        string checkoutId)
    {
        return CallCheckoutApi<Refund>(
            BaseUrl + "api/payments/" + checkoutId + "/refund", refund);
    }

    public Task<Forward?> Forward(Models.Request.Forward forward,
        string checkoutId)
    {
        return CallCheckoutApi<Forward>(
            BaseUrl + "api/payments/" + checkoutId + "/forward", forward);
    }

    protected async Task<T?> CallCheckoutApi<T>(string url, object? body)
    {
        var request = new HttpRequestMessage(body is null ? HttpMethod.Get : HttpMethod.Post, url);
        if (body is not null)
        {
            var json = JsonSerializer.Serialize(body, JsonSerializerOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        request.Headers.Authorization = AuthHeader;

        var response = await HttpClient.SendAsync(request, CancellationToken.None);
        var responseJson = await response.Content.ReadAsStringAsync();

        return JsonSerializer.Deserialize<T>(responseJson, JsonSerializerOptions);
    }
}