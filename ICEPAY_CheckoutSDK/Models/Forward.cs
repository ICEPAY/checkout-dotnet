namespace ICEPAY.CheckoutSDK.Models;

public class Forward
{
    public required string Key { get; set; }
    public Status Status { get; set; }
    public required Amount Amount { get; set; }
    public required string Reference { get; set; }
    public required string Description { get; set; }
    public Recipient? Recipient { get; set; }
}