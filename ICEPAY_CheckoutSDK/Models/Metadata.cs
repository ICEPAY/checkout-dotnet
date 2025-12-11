using System.Text.Json.Serialization;

namespace ICEPAY.CheckoutSDK.Models;

public class Metadata
{

    private readonly Dictionary<string, object?> _data;

    public Metadata(Dictionary<string, object?>? data = null)
    {
        _data = data ?? new Dictionary<string, object?>();
    }
    
    public Metadata()
    {
        _data = new Dictionary<string, object?>();
    }

    // Expose the raw data bag. The JsonExtensionData attribute makes the contents
    // appear at the top level when this object is serialized.
    [JsonExtensionData]
    public IDictionary<string, object?> Data => _data;

    public Metadata WithCustomer(Dictionary<string, object?> customer)
    {
        _data["customer"] = customer;
        return this;
    }

    public Metadata WithCustomerEmail(string email)
    {
        if (!_data.TryGetValue("customer", out var existing) || existing is not Dictionary<string, object?> customer)
        {
            customer = new Dictionary<string, object?>();
        }

        customer["email"] = email;
        _data["customer"] = customer;
        return this;
    }

    public Metadata WithIntegrationInformation(string type, string version, string developer)
    {
        Dictionary<string, object?> integration;
        if (_data.TryGetValue("integration", out var existing) && existing is Dictionary<string, object?> dict)
        {
            integration = dict;
        }
        else
        {
            integration = new Dictionary<string, object?>();
        }

        integration["type"] = type;
        integration["version"] = version;
        integration["developer"] = developer;
        _data["integration"] = integration;
        return this;
    }

    public static Metadata FromDictionary(Dictionary<string, object?> data) => new(data);
}