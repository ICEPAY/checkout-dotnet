using System.Net;

namespace Icepay;

public class IcepayException(HttpStatusCode statusCode, string body) : Exception(body)
{
    public HttpStatusCode StatusCode { get; } = statusCode;
    public string Body { get; } = body;
}