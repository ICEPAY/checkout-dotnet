using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using ICEPAY.CheckoutSDK.Models;
using ICEPAY.CheckoutSDK.Models.Request;
using ICEPAY.CheckoutSDK.Tests.Helpers;
using Xunit;

namespace ICEPAY.CheckoutSDK.Tests;

public class CheckoutClientTests_Serialization
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
        
        var client = new CheckoutClient(httpClient);

        var checkoutRequest = new Checkout
        {
            Amount = new Amount(1000, Amount.CurrencyEur),
            Description = "Test Order",
            Reference = "Order-123",
            RedirectUrl = "https://example.com",
            WebhookUrl = "https://example.com"
        };

        // Act
        var result = await client.CreateCheckout(checkoutRequest);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(Status.Started, result!.Status);
        
        // Verify other properties deserialized correctly as well
        Assert.Equal("c200aa9e-b203-455b-891d-03460775d722", result.Key);
        Assert.Equal("test@example.com", ((JsonElement)result.Meta.Data["customer"]).GetProperty("email").GetString());
        Assert.Equal(1000, result.Amount.Value);
    }
}