using Icepay.Entities;

namespace Icepay.Services;

public class PaymentCreateOptions
{
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Amount? Amount { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string WebhookUrl { get; set; } = string.Empty;
    public PaymentMethodDetails? PaymentMethod { get; set; }
    public Metadata Meta { get; set; } = new();
    public int? ExpireAfter { get; set; }
}