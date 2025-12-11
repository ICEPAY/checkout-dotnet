namespace ICEPAY.CheckoutSDK.Models;

public class Refund
{
    public string? Key { get; set; }
    public Status Status { get; set; }
    public Amount Amount { get; set; }
    public string? Reference { get; set; }
    public string? Description { get; set; }
}