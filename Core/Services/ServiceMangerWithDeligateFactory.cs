using ServicesAbstractions;

namespace Services;

public class ServiceMangerWithDeligateFactory(Func<IProductService> productFactory,
    Func<IAuthenticationService> authenticationFactory,
    Func<IBasketServices> basketFactory,
    Func<IOrderService> orderFactory,
    Func<ICacheService> CacheFactory,
    Func<IPaymentService> PaymentService)
    : IServicesManger
{
    public IProductService ProductService => productFactory.Invoke();

    public IBasketServices BasketServices => basketFactory.Invoke();

    public IAuthenticationService AuthenticationService => authenticationFactory.Invoke();

    public IOrderService OrderService => orderFactory.Invoke();
    public ICacheService cacheService => CacheFactory.Invoke();

    public IPaymentService paymentService => PaymentService.Invoke();
}
