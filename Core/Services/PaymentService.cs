using Product = Domain.Models.Product.Product;
using Microsoft.Extensions.Configuration;
using Stripe;
using Shared.SharedTransferObjects.Orders;
using Domain.Models.OrderModels;

namespace Services;

public class PaymentService(IBasketRepository basketRepository
    ,IUnitOFWork unitOFWork , IConfiguration configuration,IMapper mapper)
    : IPaymentService
{
    public async Task<BasketDTO> CreateOrUpdatePaymentIntent(string basketId)
    {
        StripeConfiguration.ApiKey = configuration.GetRequiredSection("Stripe")["SecretKey"];
        //BasketRepe => get the basket and update it
        var Basket = await basketRepository.GetAsync(basketId)
            ?? throw new BasketNotFoundExeption(basketId);

        var productRepo = unitOFWork.GetReposistory<Product>();
        foreach (var item in Basket.BasketItems)
        {
            var product = await productRepo.GetAsynce(item.id)
                ?? throw new ProductNotfoundException(item.id);
            item.Price = product.Price;
        }
        // unit of work to delivery method and products
        ArgumentNullException.ThrowIfNull(Basket.DeliveryMethodId);

        var method = await unitOFWork.GetReposistory<DeliveryMethod>()
            .GetAsynce(Basket.DeliveryMethodId.Value)
            ?? throw new DeliveryMethodNotfoundException(Basket.DeliveryMethodId.Value);
        Basket.ShippingPrice = method.Price;
        // secrit key => configration 

        var amount = (long)(Basket.BasketItems.Sum(item => item.Quantity* item.Price) + method.Price)*100;

        // Create Or Update 
        var service = new PaymentIntentService();
        if (string.IsNullOrEmpty(Basket.PaymentIntentId)) // => create
        {
            var options = new PaymentIntentCreateOptions
            {
                Amount = amount,
                Currency = "AED",
                PaymentMethodTypes = ["card"]
            };
            var paymentIntent = await service.CreateAsync(options);

            Basket.PaymentIntentId = paymentIntent.Id;
            Basket.ClientSecret = paymentIntent.ClientSecret;
        }
        else
        {
            var options = new PaymentIntentUpdateOptions()
            {
                Amount = amount
            };
            await service.UpdateAsync(Basket.PaymentIntentId, options);
        }
        await basketRepository.UpdateAsync(Basket);
        return mapper.Map<BasketDTO>(Basket);
    }
}
