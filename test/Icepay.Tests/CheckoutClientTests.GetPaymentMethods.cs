using System;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Icepay.Tests.Helpers;
using Xunit;

namespace Icepay.Tests;

public class CheckoutClient_GetPaymentMethods_Tests
{
    [Fact]
    public async Task GetPaymentMethods_ReturnsPaymentMethods()
    {
        // Arrange
        var json = "[{\"id\":\"ideal\",\"description\":\"iDEAL\"},{\"id\":\"paypal\",\"description\":\"PayPal\"},{\"id\":\"card\",\"description\":\"Card\"},{\"id\":\"bancontact\",\"description\":\"Bancontact\"}]";
        var fakeHandler = FakeHttpMessageHandler.FromJson(json);
        using var httpClient = new HttpClient(fakeHandler);
        var client = new CheckoutClient(httpClient, null);

        // Act
        var result = await client.GetPaymentMethods();

        // Assert
        Assert.NotNull(result);
        Assert.Equal(4, result!.Length);
        Assert.Equal("ideal", result.First().Id);
        Assert.Equal("iDEAL", result.First().Description);
        Assert.Null(result.First().Logo);
        Assert.NotNull(fakeHandler.LastRequest);
        Assert.Equal(HttpMethod.Get, fakeHandler.LastRequest!.Method);
        Assert.Equal("https://checkout.icepay.com/api/payments/methods", fakeHandler.LastRequest!.RequestUri!.ToString());
    }

    [Fact]
    public async Task GetPaymentMethods_UsesAuthorizationHeader_WhenProvided()
    {
        // Arrange
        var json = "[]";
        var fakeHandler = FakeHttpMessageHandler.FromJson(json);
        using var httpClient = new HttpClient(fakeHandler);
        var client = new CheckoutClient(httpClient, null);
        client.WithAuthorization("merchantId", "merchantSecret");

        // Act
        await client.GetPaymentMethods();

        // Assert
        var request = fakeHandler.LastRequest;
        Assert.NotNull(request);
        var auth = request!.Headers.Authorization;
        Assert.NotNull(auth);
        Assert.Equal("Basic", auth!.Scheme);

        // Validate that the parameter is a base64 of "merchantId:merchantSecret"
        var token = auth!.Parameter;
        Assert.False(string.IsNullOrWhiteSpace(token));
        var decoded = Encoding.UTF8.GetString(Convert.FromBase64String(token!));
        Assert.Equal("merchantId:merchantSecret", decoded);
    }
}
