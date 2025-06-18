using AutoMapper;


namespace Services;

internal class BasketService(IBasketRepository basketrepository , IMapper mapper) : IBasketServices 
{
    public async Task<bool> DeleteAsync(string id) => await basketrepository.DeleteAsync(id);
    public async Task<BasketDTO> GetAsync(string id)
    {
        var basket = await basketrepository.GetAsync(id) ??
            throw new BasketNotFoundExeption(id);
        return mapper.Map<BasketDTO>(basket); 
    }
    public async Task<BasketDTO> UpdateAsync(BasketDTO basketDTO)
    {
        var customerBasket = mapper.Map<CustomerBasket>(basketDTO);
        var updatedBasket = await basketrepository.UpdateAsync(customerBasket)
            ?? throw new Exception("Can't update Basket now!!");
        return mapper.Map<BasketDTO>(updatedBasket);
    }
}
