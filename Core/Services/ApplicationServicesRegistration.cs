using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ServicesAbstractions;

namespace Services;

public static class ApplicationServicesRegistration
{
    /// <summary>
    /// configures and adds application services to the service collection (Automapper service and serviceManger)
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddAplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(typeof(Services.AssemblyReferance).Assembly);
        services.AddScoped<IServicesManger, ServiceMangerWithDeligateFactory>();

        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IBasketServices, BasketService>();
        services.AddScoped<IOrderService, OrderService>();
        services.AddScoped<ICacheService, CacheService>();
        services.AddScoped<IPaymentService,PaymentService>();

        services.AddScoped<Func<IProductService>>(provider => ()
            => provider.GetRequiredService<IProductService>());
        services.AddScoped<Func<IAuthenticationService>>(provider => ()
            => provider.GetRequiredService<IAuthenticationService>());
        services.AddScoped<Func<IBasketServices>>(provider => ()
            => provider.GetRequiredService<IBasketServices>());
        services.AddScoped<Func<IOrderService>>(provider => ()
            => provider.GetRequiredService<IOrderService>());
        services.AddScoped<Func<ICacheService>>(provider => ()
           => provider.GetRequiredService<ICacheService>());
        services.AddScoped<Func<IPaymentService>>(provider => ()
        => provider.GetRequiredService<IPaymentService>());

        services.Configure<JWTOptions>(configuration.GetSection("JWTOptions"));
        return services;
    }
}
