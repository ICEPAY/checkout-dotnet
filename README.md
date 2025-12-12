# ICEPAY .NET Checkout SDK

This is the official .NET SDK for the [ICEPAY Checkout API](https://docs.icepay.com/developers/checkout/). For quick start, please see the [example console application](https://github.com/ICEPAY/Checkout-DotNet-SDK/tree/master/ConsoleAppExample).

## Installation

The SDK is available on [NuGet](https://www.nuget.org/packages/ICEPAY.CheckoutSDK/). To install, run the following command in the Package Manager Console:
```shell
PM> Install-Package ICEPAY.CheckoutSDK
```

## Example usage

Retrieve all payment methods:

```csharp
using ICEPAY.CheckoutSDK;

var checkoutClient = new CheckoutClient().WithAuthorization(merchantId, merchantSecret);
foreach (var paymentMethod in paymentMethods)
{
    Console.WriteLine(paymentMethod.Id + " - " + paymentMethod.Description);
}
```

Create a checkout:

```csharp
using ICEPAY.CheckoutSDK.Models;
using ICEPAY.CheckoutSDK.Models.Request;

var checkout = new Checkout()
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000, Amount.CurrencyEur),
    RedirectUrl = "https://www.example.com/redirect",
    WebhookUrl = "https://www.example.com/webhook"
}; 

checkout.WithPaymentMethod(paymentMethods[0].Id);
checkout.WithCustomerEmail("test@example.com");

var createdCheckout = await checkoutClient.CreateCheckout(checkout);
Console.WriteLine(createdCheckout.Key + " - " + createdCheckout.Links.Checkout);
```

Initiate a refund:

```csharp
var refund = new Refund()
{
    Reference = "#" + DateTime.UtcNow.Ticks,
    Description = "Test payment",
    Amount = new Amount(1000),
}
var createdRefund = await checkoutClient.Refund(refund, createdCheckout.Key);
```

## License
GNU General Public License v3.0
