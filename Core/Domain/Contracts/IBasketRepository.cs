using Domain.Models.Basket;

namespace Domain.Contracts;

public interface IBasketRepository
{
    //get basket by id
    Task<CustomerBasket> GetAsync(string id);
    //update Basket
    Task<CustomerBasket?> UpdateAsync(CustomerBasket basket , TimeSpan? TimeToLive = null);
    // detete basket
    Task<bool> DeleteAsync(string id);
}
