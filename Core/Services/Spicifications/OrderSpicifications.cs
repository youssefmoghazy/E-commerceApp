using Domain.Models.OrderModels;

namespace Services.Spicifications;

internal class OrderSpicifications
    :BaseSpefications<Order>
{
    public OrderSpicifications(Guid id)
        : base(order => order.Id  == id)
    {
        addInclude(x => x.DeliveryMethod);
        addInclude(x => x.Items);
    }
    public OrderSpicifications(string email)
        : base(order => order.BuyerEmail == email)
    {
        addInclude(x => x.DeliveryMethod);
        addInclude(x => x.Items);

        addOrderyDescending(x => x.DeliveryMethod);
    }
}
