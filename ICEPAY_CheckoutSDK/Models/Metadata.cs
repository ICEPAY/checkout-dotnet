using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ICEPAY.CheckoutSDK.Models;

public class Metadata
{
    private JsonObject _data;

    public Metadata(JsonObject? data = null)
    {
        _data = data ?? new JsonObject();
    }
    
    public Metadata() : this(null) { }

    // JsonExtensionData flattens this into the parent object on serialization
    [JsonExtensionData]
    public JsonObject Data
    {
        get => _data;
        set => _data = value ?? new JsonObject();
    }

    /// <summary>
    /// Magic indexer: automatically creates nested JsonObjects on write.
    /// </summary>
    public JsonNode? this[string key]
    {
        get => _data[key];
        set => _data[key] = value;
    }

    /// <summary>
    /// Ensures that the child node exists and is a JsonObject.
    /// Useful for nested access like meta.Ensure("customer")["email"] = ...
    /// </summary>
    public JsonObject Ensure(string key)
    {
        if (_data[key] is JsonObject obj)
            return obj;

        obj = new JsonObject();
        _data[key] = obj;
        return obj;
    }

    // --------------------------------------------------------------------
    //  Convenience builder methods
    // --------------------------------------------------------------------

    public Metadata WithCustomer(JsonObject customer)
    {
        _data["customer"] = customer;
        return this;
    }

    public Metadata WithCustomerEmail(string email)
    {
        var customer = Ensure("customer");
        customer["email"] = email;
        return this;
    }

    public Metadata WithIntegrationInformation(string type, string version, string developer)
    {
        var integration = Ensure("integration");

        integration["type"]      = type;
        integration["version"]   = version;
        integration["developer"] = developer;

        return this;
    }

    public JsonObject ToJson() => _data;
}