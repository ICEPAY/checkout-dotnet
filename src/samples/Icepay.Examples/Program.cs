using Icepay;
using Icepay.Entities;
using Icepay.Requests;

// Get yours from the ICEPAY Portal > Merchants > View Merchant.
var client = new IcepayClient("12345", "your-merchant-secret");

Payment payment = await client.Payments.CreateCheckoutAsync(new CheckoutCreateOptions
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000, Currency.Eur),
    RedirectUrl = "https://www.example.com/redirect",
    WebhookUrl = "https://www.example.com/webhook",
    PaymentMethod = new PaymentMethodDetails { Type = "ideal" },
    Meta = new Metadata().WithCustomerEmail("test@example.com"),
});

Console.Write("Send your customer to: {0}\n", payment.Links.Checkout);