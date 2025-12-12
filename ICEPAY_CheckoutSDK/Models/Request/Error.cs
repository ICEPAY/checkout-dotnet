using System.Net;

namespace ICEPAY.CheckoutSDK.Models.Request;

public class Error
{
    public HttpStatusCode Status { get; set; }
    public string Type { get; set; }
    public string Message { get; set; }
    public Dictionary<string, string[]> Errors { get; set; }
    public string Trace { get; set; }
    public Dictionary<string, string> Documentation { get; set; }
}
