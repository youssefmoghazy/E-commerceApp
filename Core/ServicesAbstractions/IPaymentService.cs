using Shared.SharedTransferObjects.Basket;

namespace ServicesAbstractions;

public interface IPaymentService
{
    Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId);
}
