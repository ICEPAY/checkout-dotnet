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

    public IcepayException(HttpStatusCode statusCode, string body, IcepayError? error = null)
        : base($"The ICEPAY API returned {(int)statusCode}: {error?.Message ?? body}")
    {
        StatusCode = statusCode;
        Body = body;
        Error = error;
    }

    public HttpStatusCode? StatusCode { get; }
    public string? Body { get; }
    public IcepayError? Error { get; }
}