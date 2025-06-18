using AutoMapper;
using Domain.Models.Product;
using Microsoft.Extensions.Configuration;
using Shared.SharedTransferObjects.Product;

namespace Services.Profiles;

public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, ProductResponce>()
            .ForMember(p => p.ProductBrand, options => options.MapFrom(s => s.productBrand.Name))
            .ForMember(p => p.ProductType, options => options.MapFrom(s => s.ProductType.Name))
            .ForMember(p => p.PictureUrl, options => options.MapFrom<PictureUrlRoslver>());
        CreateMap<ProductBrand, BrandResponce>();
        CreateMap<ProductType,TypeResponce>();
    }
}
internal class PictureUrlRoslver (IConfiguration configuration)
    : IValueResolver<Product, ProductResponce, string>
{
    public string Resolve(Product source, ProductResponce destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return $"{configuration["BaseUrl"]}{source.PictureUrl}";
        }
        return "";
    }
}
