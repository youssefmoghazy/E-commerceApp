using Shared.SharedTransferObjects.Orders;

namespace ServicesAbstractions;

public interface IOrderService
{
    Task<OrderResponce> CreateAsync(OrderRequest request, string email);
    Task<OrderResponce> GetAsync (Guid id);
    Task<IEnumerable<OrderResponce>> GetAllAsync (string Email);
    Task<IEnumerable<DeliverymethodResponce>> GetDeliveryMethodAsync ();
}
