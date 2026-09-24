using System.Net;

namespace ICEPAY.Checkout;

public class IcepayException : Exception
{
    public IcepayException(string message)
        : base(message)
    {
    }

    public IcepayException(string message, Exception innerException)
        : base(message, innerException)
    {
    }

    public IcepayException(HttpStatusCode statusCode, string body)
        : base($"The ICEPAY API returned {(int)statusCode}: {body}")
    {
        StatusCode = statusCode;
        Body = body;
    }

    public HttpStatusCode? StatusCode { get; }
    public string? Body { get; }
}