using Shared.SharedTransferObjects.Orders;
using Domain.Models.OrderModels;
using Domain.Models.Product;
using Services.Spicifications;
namespace Services;

public class OrderService (IMapper mapper, IUnitOFWork unitOFWork,IBasketRepository basketRepository)
    : IOrderService
{
    public async Task<OrderResponce> CreateAsync(OrderRequest request, string email)
    {
        //ShipToAddress
        var address = mapper.Map<OrderAddress>(request.shipToAddress);
        //Basket to get the items from
        var basket = await basketRepository.GetAsync(request.BasketId)??
            throw new BasketNotFoundExeption(request.BasketId);


        var orderRepo = unitOFWork.GetReposistory<Order, Guid>();
        var existingOrder = await orderRepo.GetAsynce(new OrderWithPaymentIntentSpecification(basket.PaymentIntentId));

        if (existingOrder != null)
            orderRepo.Delete(existingOrder);
        List<OrderItem> items = [];
        var ProductRepo = unitOFWork.GetReposistory<Product>();
        foreach(var item in basket.BasketItems)
        {
            var product = await ProductRepo.GetAsynce(item.id)??
                throw new ProductNotfoundException (item.id);

            items.Add(CreateOrderItem(product,item));
            item.Price = product.Price;
        }
        // Delivery Method
        var method = await unitOFWork.GetReposistory<DeliveryMethod>()
            .GetAsynce(request.DeliveryMethodId)??
            throw new DeliveryMethodNotfoundException(request.DeliveryMethodId);

        var subtotal = items.Sum(x => x.Price * x.Quantity);
        var order = new Order(email, items,address, method ,subtotal,basket.PaymentIntentId);
        orderRepo.Add(order);

        await unitOFWork.SaveChangesAsynce();
        
        return mapper.Map<OrderResponce>(order);
    }

    private static OrderItem CreateOrderItem(Product product, BasketItem item)
        => new OrderItem(new(product.Id,product.Name,product.PictureUrl), product.Price, item.Quantity);

    public async Task<IEnumerable<OrderResponce>> GetAllAsync(string Email)
    {
        var orders = await unitOFWork.GetReposistory<Order, Guid>()
            .GetAllAsynce(new OrderSpicifications(Email));

        return mapper.Map<IEnumerable<OrderResponce>>(orders);
    }

    public async Task<OrderResponce> GetAsync(Guid id)
    {
        var orders = await unitOFWork.GetReposistory<Order, Guid>()
            .GetAsynce(new OrderSpicifications(id));

        return mapper.Map<OrderResponce>(orders);
    }

    public async Task<IEnumerable<DeliverymethodResponce>> GetDeliveryMethodAsync()
    {
        var deliveryMethods = await unitOFWork.GetReposistory<DeliveryMethod>().GetAllAsynce();
        return mapper.Map<IEnumerable<DeliverymethodResponce>>(deliveryMethods);
    }
}
