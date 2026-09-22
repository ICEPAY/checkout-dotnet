using Icepay.Entities;

namespace Icepay.Services;

public class ForwardCreateOptions
{
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Recipient Recipient { get; set; } = new();
    public Amount Amount { get; set; } = new();
}