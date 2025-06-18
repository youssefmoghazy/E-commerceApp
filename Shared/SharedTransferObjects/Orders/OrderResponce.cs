using Shared.SharedTransferObjects.Authentication;
namespace Shared.SharedTransferObjects.Orders;
public record OrderResponce
{
    public Guid id {  get; set; }
    public string BuyerEmail { get; set; } = default!;
    public DateTimeOffset OrderDate { get; set; }
    public IEnumerable<OrderItemDTO> Items { get; set; } = default!;
    public AddressDTO ShipToAddress { get; set; } = default!;
    public string DeliveryMethod { get; set; } = default!;
    public string Status { get; set; } = default!;
    public string PaymentIntenId { get; set; } = string.Empty;
    public decimal Subtotal { get; set; }
    public decimal Total { get; set; }
    public decimal DeliveryCost { get; set; }

}
public record OrderItemDTO
{
    public int ProductId { get; set; }
    public string PictureUrl { get; set; } = default!;
    public string ProductName { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
}