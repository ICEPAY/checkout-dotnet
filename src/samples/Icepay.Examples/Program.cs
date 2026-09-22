using Icepay;
using Icepay.Entities;
using Icepay.Requests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

using IHost host = Host.CreateDefaultBuilder(args).UseEnvironment("Development").Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var merchantId = config["ICEPAY:MERCHANT_ID"] ?? throw new InvalidOperationException("ICEPAY:MERCHANT_ID is not configured.");
var merchantSecret = config["ICEPAY:MERCHANT_SECRET"] ?? throw new InvalidOperationException("ICEPAY:MERCHANT_SECRET is not configured.");

var client = new IcepayClient(merchantId, merchantSecret);

var paymentMethods = await client.PaymentMethods.ListAsync();

foreach (var paymentMethod in paymentMethods)
{
    Console.WriteLine(paymentMethod.Id + " - " + paymentMethod.Description);
}

var options = new PaymentCreateOptions
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000, Currency.Eur),
    RedirectUrl = "https://www.example.com/redirect",
    WebhookUrl = "https://www.example.com/webhook",
    PaymentMethod = new() { Type = "ideal" },
};
options.Meta.WithCustomerEmail("test@example.com");

var payment = await client.Payments.CreateAsync(options);

Console.WriteLine(payment.Key + " - " + payment.Links.Checkout);
