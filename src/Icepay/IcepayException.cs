using System.Net;

namespace Icepay;

public class IcepayException : Exception
{
    public IcepayException(string message)
        : base(message)
    {
    }

    public IcepayException(HttpStatusCode statusCode, string body)
        : base(body)
    {
        StatusCode = statusCode;
        Body = body;
    }

    public HttpStatusCode? StatusCode { get; }
    public string? Body { get; }
}