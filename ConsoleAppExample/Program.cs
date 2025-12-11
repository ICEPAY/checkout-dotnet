using ICEPAY.CheckoutSDK;
using ICEPAY.CheckoutSDK.Models;
using ICEPAY.CheckoutSDK.Models.Request;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

Console.WriteLine("Hello, World!");


using IHost host = Host.CreateDefaultBuilder(args).UseEnvironment("Development").Build();

// Ask the service provider for the configuration abstraction.
IConfiguration config = host.Services.GetRequiredService<IConfiguration>();

var merchantId = config["ICEPAY:MERCHANT_ID"];
var merchantSecret = config["ICEPAY:MERCHANT_SECRET"];

var checkoutClient = new CheckoutClient().WithAuthorization(merchantId, merchantSecret);


var paymentMethods = await checkoutClient.GetPaymentMethods();

foreach (var paymentMethod in paymentMethods)
{
    Console.WriteLine(paymentMethod.Id + " - " + paymentMethod.Description);
}

var checkout = new Checkout()
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000, Amount.CurrencyEur),
    RedirectUrl = "https://www.example.com/redirect",
    WebhookUrl = "https://www.example.com/webhook"
}; 

checkout.WithPaymentMethod("ideal");
// checkout.WithCustomerEmail("test@example.com");

var createdCheckout = await checkoutClient.CreateCheckout(checkout);

Console.WriteLine(createdCheckout.Key + " - " + createdCheckout.Links.Checkout);
