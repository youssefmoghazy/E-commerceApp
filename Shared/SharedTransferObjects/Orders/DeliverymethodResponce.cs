using System.Collections.Specialized;

namespace Shared.SharedTransferObjects.Orders;

public record DeliverymethodResponce
{
    public int Id { get; set; }
    public string ShortName { get; set; } = default!;
    public string Description { get; set; } = default!;
    public String DeliveryTime { get; set; } = default!;
    public decimal Cost { get; set; }
}
