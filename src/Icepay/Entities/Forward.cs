namespace Icepay.Entities;

public class Forward
{
    public string Key { get; set; } = string.Empty;
    public Status Status { get; set; }
    public Amount Amount { get; set; } = new();
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Recipient? Recipient { get; set; }
    public Payment? Payment { get; set; }
}