namespace ICEPAY.CheckoutSDK.Models;

public class Payment
{
    public string Key { get; set; } = string.Empty;
    public Status Status { get; set; }
    public Amount Amount { get; set; } = new();
    public PaymentMethod? PaymentMethod { get; set; }
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Metadata? Meta { get; set; }
    public string? WebhookUrl { get; set; }
    public string? RedirectUrl { get; set; }
    public Merchant Merchant { get; set; } = new();
    public bool IsTest { get; set; }
    public DateTime? ExpireAfter { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public Links Links { get; set; } = new();
}