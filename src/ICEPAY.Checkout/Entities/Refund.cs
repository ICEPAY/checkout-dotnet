namespace ICEPAY.Checkout;

public class Refund
{
    public string? Key { get; set; }
    public Status Status { get; set; }
    public Amount Amount { get; set; } = new();
    public string? Reference { get; set; }
    public string? Description { get; set; }
    public Payment? Payment { get; set; }
}