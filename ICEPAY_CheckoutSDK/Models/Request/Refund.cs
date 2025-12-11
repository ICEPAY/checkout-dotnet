namespace ICEPAY.CheckoutSDK.Models.Request;

public class Refund(string reference, Amount amount, string? description = null)
{
   public string Reference { get; set; }
   public string Description { get; set; }
   public Amount Amount { get; set; }
   
   public Refund WithReference(string reference)
   {
      Reference = reference;
      return this;
   }

   public Refund WithDescription(string description)
   {
      Description = description;
      return this;
   }
    
   public Refund WithAmount(Amount amount)
   {
      Amount = amount;
      return this;
   }

   public Refund WithAmount(int amount)
   {
      return WithAmount(new Amount(amount));
   }
}