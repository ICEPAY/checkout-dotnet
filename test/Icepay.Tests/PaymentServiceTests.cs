using Icepay.Entities;
using Icepay.Requests;
using Icepay.Tests.Helpers;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Icepay.Tests;

public class PaymentServiceTests
{
    [Fact]
    public async Task CreateCheckout_DeserializesStringEnum_Correctly()
    {
        // Arrange
        var jsonResponse = """
            {
              "key": "c200aa9e-b203-455b-891d-03460775d722",
              "status": "started",
              "amount": { "value": 1000, "currency": "eur" },
              "reference": "Order-123",
              "description": "Test Order",
              "merchant": { "id": 12345, "name": "Test Merchant" },
              "meta": { "customer": { "email": "test@example.com" } },
              "links": { "checkout": "https://checkout.icepay.com/pay", "direct": null, "documentation": null }
            }
            """;

        var fakeHandler = FakeHttpMessageHandler.FromJson(jsonResponse);
        using var httpClient = new HttpClient(fakeHandler);

        var client = new IcepayClient(new IcepayClientOptions
        {
            MerchantId = "merchant",
            MerchantSecret = "secret",
            HttpClient = httpClient,
        });

        var options = new CheckoutCreateOptions
        {
            Amount = new Amount(1000, Currency.Eur),
            Description = "Test Order",
            Reference = "Order-123",
            Meta = new Metadata()
                .WithIntegrationInformation("icepay-checkout-sdk", "1.0.0", "icepay-checkout-sdk-tests")
                .WithCustomerEmail("test@example.com"),
            RedirectUrl = "https://example.com",
            WebhookUrl = "https://example.com",
        };

        // Act
        var result = await client.Payments.CreateCheckoutAsync(options);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Status.Started, result!.Status);

        // Verify other properties deserialized correctly as well
        Assert.Equal("c200aa9e-b203-455b-891d-03460775d722", result.Key);
        Assert.Equal("test@example.com", result.Meta!["customer"]!["email"]!.ToString());
        Assert.Equal(1000, result.Amount.Value);
    }

    [Fact]
    public async Task CreateCheckout_Throws_WhenResponseIsNotJson()
    {
        using var httpClient = new HttpClient(FakeHttpMessageHandler.FromJson("<html>502</html>"));
        var client = new IcepayClient(new IcepayClientOptions
        {
            MerchantId = "merchant",
            MerchantSecret = "secret",
            HttpClient = httpClient,
        });

        await Assert.ThrowsAsync<IcepayException>(() => client.Payments.GetAsync("pi-1"));
    }

    [Fact]
    public async Task CreateCheckout_Throws_WhenResponseIsEmpty()
    {
        using var httpClient = new HttpClient(FakeHttpMessageHandler.FromJson("", HttpStatusCode.Unauthorized));
        var client = new IcepayClient(new IcepayClientOptions
        {
            MerchantId = "merchant",
            MerchantSecret = "secret",
            HttpClient = httpClient,
        });

        var exception = await Assert.ThrowsAsync<IcepayException>(() => client.Payments.GetAsync("pi-1"));
        Assert.Equal(HttpStatusCode.Unauthorized, exception.StatusCode);
    }
}