namespace Icepay.Requests;

public class RefundCreateOptions
{
    public string Reference { get; set; } = string.Empty;
    public string? Description { get; set; }
    public Amount Amount { get; set; } = new();
}