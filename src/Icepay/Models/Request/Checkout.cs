using System.Text.Json.Nodes;

namespace ICEPAY.CheckoutSDK.Models.Request;

public class Checkout
{
    public string Reference { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Amount? Amount { get; set; }
    public string RedirectUrl { get; set; } = string.Empty;
    public string WebhookUrl { get; set; } = string.Empty;
    public PaymentMethod? PaymentMethod { get; set; }
    public Metadata Meta { get; set; } = new();
    public int? ExpireAfter { get; set; }

    public Checkout WithReference(string reference)
    {
        Reference = reference;
        return this;
    }

    public Checkout WithDescription(string description)
    {
        Description = description;
        return this;
    }

    public Checkout WithRedirectUrl(string redirectUrl)
    {
        RedirectUrl = redirectUrl;
        return this;
    }

    public Checkout WithWebhookUrl(string webhookUrl)
    {
        WebhookUrl = webhookUrl;
        return this;
    }

    public Checkout WithAmount(Amount amount)
    {
        Amount = amount;
        return this;
    }

    public Checkout WithPaymentMethod(PaymentMethod paymentMethod)
    {
        PaymentMethod = paymentMethod;
        return this;
    }

    public Checkout WithPaymentMethod(string paymentMethodType)
    {
        return WithPaymentMethod(new PaymentMethod { Type = paymentMethodType });
    }

    public Checkout WithExpireAfter(int? expireAfter)
    {
        ExpireAfter = expireAfter;
        return this;
    }
    
    public Checkout WithCustomer(JsonObject customer)
    {
        Meta.WithCustomer(customer);
        return this;
    }
    
    public Checkout WithCustomerEmail(string email)
    {
        Meta.WithCustomerEmail(email);
        return this;
    }
    
    public Checkout WithIntegrationInformation(string type, string version, string developer)
    {
        Meta.WithIntegrationInformation(type, version, developer);
        return this;
    }
}