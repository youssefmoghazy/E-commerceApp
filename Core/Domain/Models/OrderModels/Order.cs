namespace Domain.Models.OrderModels;

public class Order : BaseEntity<Guid>
{
    public Order()
    {
        
    }
    public Order(string email, IEnumerable<OrderItem> items, OrderAddress address,
        DeliveryMethod deliveryMethod,
        decimal subtotal, string paymentIntentId)
    {
        BuyerEmail = email;
        Items = items.ToList();
        ShipToAddress = address;
        DeliveryMethod = deliveryMethod;
        this.subtotal = subtotal;
        PaymentIntentId = paymentIntentId;
    }

    //id
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    public OrderAddress ShipToAddress { get; set; } = default!;
    public DeliveryMethod DeliveryMethod { get; set; } = default!;
    public int DeliverymethodId { get; set; }
    public PaymentStatus Status { get; set; } = PaymentStatus.Pending;
    public string PaymentIntentId { get; set; } = default!;
    public decimal subtotal { get; set; }
}
