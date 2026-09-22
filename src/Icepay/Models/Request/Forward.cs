namespace ICEPAY.CheckoutSDK.Models.Request;

public class Forward
{
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Recipient Recipient { get; set; } = new();
    public Amount Amount { get; set; } = new();

    public Forward WithReference(string reference)
    {
        Reference = reference;
        return this;
    }

    public Forward WithDescription(string description)
    {
        Description = description;
        return this;
    }
    
    public Forward WithAmount(Amount amount)
    {
        Amount = amount;
        return this;
    }

    public Forward WithAmount(int amount)
    {
        return WithAmount(new Amount(amount));
    }

    public Forward WithRecipient(Recipient recipient)
    {
        Recipient = recipient;
        return this;
    }

    public Forward WithRecipient(string recipient)
    {
        return WithRecipient(new Recipient(){Id = recipient});
    }
}