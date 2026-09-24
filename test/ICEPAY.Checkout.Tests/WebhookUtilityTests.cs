using Xunit;

namespace ICEPAY.Checkout.Tests;

public class WebhookUtilityTests
{
    private const string Secret = "your-merchant-secret";
    private const string Json = """{"key":"pi-01j1pta7ymwcjk25q4rtpqmn2q","status":"started","amount":{"value":299,"currency":"eur"}}""";
    private const string Signature = "JGuqmdMKfFGNIPGrT5gOoNp+Pk+Zk5npqdJnxf41b/0=";

    [Fact]
    public void ConstructEvent_ReturnsPayment_WhenSignatureIsValid()
    {
        var payment = WebhookUtility.ConstructEvent(Json, Signature, Secret);

        Assert.Equal("pi-01j1pta7ymwcjk25q4rtpqmn2q", payment.Key);
        Assert.Equal(Status.Started, payment.Status);
    }

    [Fact]
    public void ValidateSignature_Throws_WhenSignatureIsInvalid()
    {
        Assert.Throws<IcepayException>(
            () => WebhookUtility.ValidateSignature(Json, Signature, "wrongSecret"));
    }

    [Fact]
    public void ValidateSignature_Throws_WhenHeaderIsMissing()
    {
        Assert.Throws<IcepayException>(
            () => WebhookUtility.ValidateSignature(Json, "", Secret));
    }
}