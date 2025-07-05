using Domain.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistanc;
using Persistance.Identity;
using Persistance.Repositories;
using StackExchange.Redis;

namespace Persistance;
 
public static class InfrastructtureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configration)
    {

        services.AddDbContext<StoreDbContext>(options => options.UseSqlServer(configration.GetConnectionString("DefaultConnection")));
        services.AddDbContext<StoreIdentityDBContext>(options => options.UseSqlServer(configration.GetConnectionString("IdentityConnection")));

        services.AddSingleton<IConnectionMultiplexer>(_ =>
        {
            var redisConnectionString = configration.GetConnectionString("RedisConnection");

            var config = ConfigurationOptions.Parse(redisConnectionString!);
            config.AbortOnConnectFail = false;

            return ConnectionMultiplexer.Connect(config);
        });

        services.AddScoped<IDbInitializer, DbInitializer>();
        services.AddScoped<IBasketRepository, BasketRepository>();
        services.AddScoped<IcacheRepository, CacheRepository>();
        services.AddScoped<IUnitOFWork, UnitOfWork>();
        ConfigerIdentity(services, configration);
        return services;
    }

    private static void ConfigerIdentity(IServiceCollection services, IConfiguration configration)
    {
        services.AddIdentityCore<ApplicationUser>(config =>
            {
                config.User.RequireUniqueEmail = true;
                config.Password.RequireLowercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<StoreIdentityDBContext>();
    }
}
