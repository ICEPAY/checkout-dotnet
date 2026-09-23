# ICEPAY

The official .NET library for the ICEPAY [Checkout API](https://docs.icepay.com/developers/checkout/).

## Installation

```sh
dotnet add package icepay-dotnet
```

## Requirements

We currently support .NET Standard 2.0+, .NET 6+, and .NET Framework 4.6.2+.

## Documentation

For full information and a list of examples please check the [documentation](https://docs.icepay.com/developers/checkout/).

## Usage

The `IcepayClient` class is the entry point to all services.

```csharp
var client = new IcepayClient("12345", "your-merchant-secret");
```

With your own `HttpClient`:

```csharp
var client = new IcepayClient(new IcepayClientOptions
{
    MerchantId = "12345",
    MerchantSecret = "your-merchant-secret",
    HttpClient = httpClient,
});
```

## Authentication

The Checkout API uses `Basic authentication`: your Merchant ID is the _username_, your Merchant Secret is the _password_.

Both can be obtained from the [ICEPAY Portal](https://portal.icepay.com) > Merchants > View Merchant.

`IcepayClient` builds the `Authorization` header for you:

```csharp
var client = new IcepayClient("12345", "your-merchant-secret");
```

## Environment

A merchant (usually a website) has two processing modes: **test** or **live**. By default, a Merchant is in test mode, changed in the [ICEPAY Portal](https://portal.icepay.com).

Every payment tells you which mode it was processed in:

```csharp
if (payment.IsTest)
{
    // Payment is in test mode
}
```

## Examples

### Create checkout

```csharp
var payment = await client.Payments.CreateCheckoutAsync(new CheckoutCreateOptions
{
    Reference = "OR-123412",
    Description = "Ice cream with sprinkles",
    Amount = new Amount(299, Currency.Eur),
    RedirectUrl = "https://example.com/redirect",
    WebhookUrl = "https://example.com/webhook",
    PaymentMethod = new PaymentMethodDetails { Type = "ideal" },
    Meta = new Metadata().WithCustomerEmail("test@example.com"),
});
```

Send your customer to `payment.Links.Checkout` to complete the payment:

```text
https://checkout.icepay.com/checkout/pi-01j1ps8zf4jgnk0c3dnd477sp1
```

### Retrieve a payment

```csharp
var payment = await client.Payments.GetAsync("pi-01j1ps8zf4jgnk0c3dnd477sp1");

if (payment.Status == Status.Completed)
{
    // Mark as paid
}
```

### Webhook

When a payment changes status, a request is sent to the provided webhook url.

`ConstructEvent` validates the `ICEPAY-Signature` header and returns the `Payment`, or throws `IcepayException`.

```csharp
[HttpPost("webhook")]
public async Task<IActionResult> Webhook()
{
    var json = await new StreamReader(Request.Body).ReadToEndAsync();

    try
    {
        var payment = WebhookUtility.ConstructEvent(json, Request.Headers[WebhookUtility.SignatureHeader], merchantSecret);
        return Ok();
    }
    catch (IcepayException)
    {
        return BadRequest();
    }
}
```

> Read the raw body, not `[FromBody]`, or the signature won't match.

### Refund a payment

```csharp
var refund = await client.Refunds.CreateAsync(payment.Key, new RefundCreateOptions
{
    Reference = "RFD-00069",
    Description = "Received choco dip instead of sprinkles",
    Amount = new Amount(299, Currency.Eur),
});
```

### Forward a payment

With Payment Forwarding, you can transfer part of the funds to another ICEPAY Account. Read more on the [Payment Forwarding page](https://docs.icepay.com/products/payment-forwarding/payment-forwarding/).

```csharp
var forward = await client.Forwards.CreateAsync(payment.Key, new ForwardCreateOptions
{
    Reference = "FWD-00042",
    Description = "Forwarding to merchant 1001",
    Amount = new Amount(299, Currency.Eur),
    Recipient = new Recipient { Id = "1001" },
});
```

## Metadata

Key-value pairs that allow you to store more data when creating a checkout.

### Customer

You can add the following properties to the customer object when creating a checkout.

```csharp
Meta = new Metadata().With("customer", new JsonObject
{
    ["email"] = "info@icepay.com",
    ["firstName"] = "Jane",
    ["lastName"] = "Doe",
    ["ip"] = "127.0.0.1",
    ["address"] = new JsonObject { ["city"] = "amsterdam", ["country"] = "nl" },
}),
```

It is returned in the payment and in the webhook.

### Redirect

Redirect the customer back to your shop instead of the ICEPAY Payment Page.

```csharp
Meta = new Metadata().With("redirectBackToMerchant", true),
```

The request property takes precedence over the [ICEPAY Portal](https://portal.icepay.com) setting.

## License

This repository is available under the MIT license.
