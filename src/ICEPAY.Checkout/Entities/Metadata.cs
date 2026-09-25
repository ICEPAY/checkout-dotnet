using System.Text.Json.Nodes;

namespace ICEPAY.Checkout;

public sealed class Metadata : Dictionary<string, JsonNode?>
{
    public Metadata With(string key, JsonNode? value)
    {
        this[key] = value;
        return this;
    }

    public Metadata WithCustomerEmail(string email)
        => With("customer", new JsonObject
        {
            ["email"] = email
        });

    public Metadata WithIntegrationInformation(string type, string version, string developer)
        => With("integration", new JsonObject
        {
            ["type"] = type,
            ["version"] = version,
            ["developer"] = developer,
        });

    public JsonNode? Get(string key)
        => TryGetValue(key, out var node) ? node : null;
}