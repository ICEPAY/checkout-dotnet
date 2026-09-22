using Icepay;
using Icepay.Entities;
using Icepay.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");

using IHost host = Host.CreateDefaultBuilder(args).UseEnvironment("Development").Build();
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var merchantId = config["ICEPAY:MERCHANT_ID"];
var merchantSecret = config["ICEPAY:MERCHANT_SECRET"];

var checkoutClient = new CheckoutClient().WithAuthorization(merchantId, merchantSecret);


var paymentMethods = await checkoutClient.GetPaymentMethods();

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

var createdCheckout = await checkoutClient.CreateCheckout(options);

Console.WriteLine(createdCheckout.Key + " - " + createdCheckout.Links.Checkout);
