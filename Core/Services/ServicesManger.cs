using AutoMapper;
using Domain.Contracts;
using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using ServicesAbstractions;

namespace Services;

public class ServicesManger(IUnitOFWork unitOFWork , IMapper mapper ,
    IBasketRepository basketRepository,
    UserManager<ApplicationUser> userManager,IOptions<JWTOptions> options) 
    //: IServicesManger
{
    private readonly Lazy<IProductService> productService = new Lazy<IProductService>(() => new ProductService(unitOFWork , mapper));
    public IProductService ProductService => productService.Value;


    private readonly Lazy<IBasketServices> BasketService = new Lazy<IBasketServices>(() => new BasketService(basketRepository, mapper));
    public IBasketServices BasketServices => BasketService.Value;


    private readonly Lazy<IAuthenticationService> UserService = new Lazy<IAuthenticationService>(() => new AuthenticationService(userManager,options,mapper));
    public IAuthenticationService AuthenticationService => UserService.Value;

    private readonly Lazy<IOrderService> orderService = new Lazy<IOrderService>(() => new OrderService(mapper,unitOFWork,basketRepository));
    public IOrderService OrderService => orderService.Value;
}
