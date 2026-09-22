using Icepay.Entities;

namespace Icepay.Services;

public class RefundCreateOptions(string reference, Amount amount, string? description = null)
{
    public string Reference { get; set; } = reference;
    public string? Description { get; set; } = description;
    public Amount Amount { get; set; } = amount;
}