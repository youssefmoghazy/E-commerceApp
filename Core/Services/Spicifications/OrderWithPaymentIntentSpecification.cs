using Domain.Models.OrderModels;

namespace Services.Spicifications;

public class OrderWithPaymentIntentSpecification(string paymentIntentId)
    :BaseSpefications<Order>(order => order.PaymentIntentId == paymentIntentId)
{

}
