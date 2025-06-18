namespace ServicesAbstractions;

public interface IServicesManger
{
    IProductService ProductService { get;}
    IBasketServices BasketServices { get;}
    IAuthenticationService AuthenticationService { get;}
    IOrderService OrderService { get;}
    ICacheService cacheService { get;}
    IPaymentService paymentService { get;}

}
