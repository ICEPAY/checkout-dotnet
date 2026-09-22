namespace ICEPAY.CheckoutSDK.Models;

public class Payment
{
    public required string Key { get; set; }
    public Status Status { get; set; }
    public required Amount Amount { get; set; }
    public PaymentMethod? PaymentMethod { get; set; }
    public required string Reference { get; set; }
    public required string Description { get; set; }
    public Metadata? Meta { get; set; }
    public string? WebhookUrl { get; set; }
    public string? RedirectUrl { get; set; }
    public required Merchant Merchant { get; set; }
    public bool IsTest { get; set; }
    public DateTime? ExpireAfter { get; set; }
    public DateTime? CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public required Links Links { get; set; }
}