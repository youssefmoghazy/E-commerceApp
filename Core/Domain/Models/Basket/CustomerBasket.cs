namespace Domain.Models.Basket;

public class CustomerBasket
{
    public string id { get; set; } // create from the client
    public ICollection<BasketItem> BasketItems { get; set; } = [];
    public string? PaymentIntentId { get; set; }
    public string? ClientSecret { get; set; }
    public int? DeliveryMethodId { get; set; }
    public decimal ShippingPrice { get; set; }
}
