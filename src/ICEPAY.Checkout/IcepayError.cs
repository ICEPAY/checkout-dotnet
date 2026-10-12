namespace ICEPAY.Checkout;

public class IcepayError
{
    public string? Type { get; set; }
    public string? Message { get; set; }
    public Dictionary<string, string[]>? Errors { get; set; }
    public string? Trace { get; set; }
    public Dictionary<string, string>? Documentation { get; set; }
}