using Icepay;
using Icepay.Entities;
using Icepay.Requests;

// Don't embed any credentials in production code. This is an example.
// Get yours from the ICEPAY Portal > Merchants > View Merchant.
var client = new IcepayClient("12345", "your-merchant-secret");

PaymentMethod[] paymentMethods = await client.PaymentMethods.ListAsync();
Console.Write("Success! Here are your enabled payment methods: {0}\n", string.Join(", ", paymentMethods.Select(m => m.Id)));

var optionsPayment = new PaymentCreateOptions
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000, Currency.Eur),
    RedirectUrl = "https://www.example.com/redirect",
    WebhookUrl = "https://www.example.com/webhook",
    PaymentMethod = new PaymentMethodDetails { Type = "ideal" },
    Meta = new Metadata().WithCustomerEmail("test@example.com"),
};
Payment payment = await client.Payments.CreateAsync(optionsPayment);
Console.Write("Success! Here is your payment key: {0}\n", payment.Key);
Console.Write("Send your customer to: {0}\n", payment.Links.Checkout);