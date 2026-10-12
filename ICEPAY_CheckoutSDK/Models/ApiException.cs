using System.Net;
using ICEPAY.CheckoutSDK.Models.Request;

namespace ICEPAY.CheckoutSDK.Models;

public class ApiException(string message, Exception innerException, HttpStatusCode status): HttpRequestException(message, innerException, status)
{
    public Error Error { get; set; }
    public ApiException(Error error) : this(error.Message, null, error.Status)
    {
        Error = error;
    }
}
