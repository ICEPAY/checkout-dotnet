using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace ICEPAY.CheckoutSDK.Models;

public class Metadata
{

    private readonly JsonObject _data;

    public Metadata(JsonObject? data = null)
    {
        _data = data ?? new JsonObject();
    }
    
    public Metadata()
    {
        _data = new JsonObject();
    }

    // Expose the raw data bag. The JsonExtensionData attribute makes the contents
    // appear at the top level when this object is serialized.
    [JsonExtensionData]
    public JsonObject Data => _data;

    public Metadata WithCustomer(JsonObject customer)
    {
        _data["customer"] = customer;
        return this;
    }

    public Metadata WithCustomerEmail(string email)
    {
        if (!_data.ContainsKey("customer") || _data["customer"] is not JsonObject customer)
        {
            customer = new JsonObject();
        }

        customer["email"] = email;
        _data["customer"] = customer;
        return this;
    }

    public Metadata WithIntegrationInformation(string type, string version, string developer)
    {
        JsonObject integration;
        if (_data.ContainsKey("integration") && _data["integration"] is JsonObject dict)
        {
            integration = dict;
        }
        else
        {
            integration = new JsonObject();
        }

        integration["type"] = type;
        integration["version"] = version;
        integration["developer"] = developer;
        _data["integration"] = integration;
        return this;
    }
    
    public JsonNode? this[string key]
    {
        get => Data[key];
        set => Data[key] = value;
    }

    // public static Metadata FromDictionary(IDictionary<string, object?> dict)
    // {
    //     return new Metadata(JsonObject.Create(dict));
    // }
    public JsonObject ToJson() => Data;
}