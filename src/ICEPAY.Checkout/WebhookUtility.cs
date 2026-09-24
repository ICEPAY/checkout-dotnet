using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace ICEPAY.Checkout;

public static class WebhookUtility
{
    public const string SignatureHeader = "ICEPAY-Signature";

    public static Payment ConstructEvent(string json, string? signature, string secret)
    {
        ValidateSignature(json, signature, secret);

        return ParseEvent(json);
    }

    public static Payment ParseEvent(string json)
        => JsonSerializer.Deserialize<Payment>(json, JsonSerialization.Options)
            ?? throw new IcepayException("The webhook body is empty.");

    public static void ValidateSignature(string json, string? signature, string secret)
    {
        if (signature is null or "")
            throw new IcepayException($"The {SignatureHeader} header is missing.");

        if (!StringUtils.SecureEquals(ComputeSignature(json, secret), signature))
            throw new IcepayException("The webhook signature does not match the expected signature.");
    }

    private static string ComputeSignature(string json, string secret)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(secret));

        return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(json)));
    }
}