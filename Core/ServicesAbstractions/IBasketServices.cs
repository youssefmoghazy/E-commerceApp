using Shared.SharedTransferObjects.Basket;

namespace ServicesAbstractions;

public interface IBasketServices
{
    Task<BasketDTO> GetAsync (string id);
    Task<BasketDTO> UpdateAsync (BasketDTO basketDTO);
    Task<bool> DeleteAsync (string id);
}
