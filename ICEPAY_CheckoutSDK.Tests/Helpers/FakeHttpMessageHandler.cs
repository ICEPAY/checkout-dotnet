using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ICEPAY.CheckoutSDK.Tests.Helpers;

internal class FakeHttpMessageHandler : HttpMessageHandler
{
    private readonly HttpResponseMessage _response;

    public HttpRequestMessage? LastRequest { get; private set; }

    public FakeHttpMessageHandler(HttpResponseMessage response)
    {
        _response = response;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        LastRequest = request;
        return Task.FromResult(_response);
    }

    public static FakeHttpMessageHandler FromJson(string json, HttpStatusCode statusCode = HttpStatusCode.OK)
    {
        var message = new HttpResponseMessage(statusCode)
        {
            Content = new StringContent(json)
        };
        message.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
        return new FakeHttpMessageHandler(message);
    }
}
