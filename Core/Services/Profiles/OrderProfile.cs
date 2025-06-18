using Domain.Models.OrderModels;
using Shared.SharedTransferObjects.Orders;
using Microsoft.Extensions.Configuration;

namespace Services.Profiles;

internal class OrderProfile : Profile
{
    public OrderProfile()
    {
        CreateMap<OrderAddress, AddressDTO>().ReverseMap();

        CreateMap<OrderItem,OrderItemDTO>()
            .ForMember(d => d.ProductName, o => o.MapFrom(s => s.Product.Productname))
            .ForMember(d => d.ProductId, o => o.MapFrom(s => s.Product.Productid))
            .ForMember(d => d.PictureUrl , o => o.MapFrom<OrderItemPictureUrlResolver>());

        CreateMap<Order, OrderResponce>()
            .ForMember(d => d.DeliveryMethod, o => o.MapFrom(s => s.DeliveryMethod.ShortName))
            .ForMember(d => d.Total, o => o.MapFrom(s => s.DeliveryMethod.Price + s.subtotal))
            .ForMember(d => d.DeliveryCost, opt => opt.MapFrom(s => s.DeliveryMethod.Price));

        CreateMap<DeliveryMethod, DeliverymethodResponce>()
            .ForMember(d => d.Cost, o => o.MapFrom(s => s.Price));
    }
}
internal class OrderItemPictureUrlResolver(IConfiguration configuration)
    : IValueResolver<OrderItem, OrderItemDTO, string>
{
    public string Resolve(OrderItem source, OrderItemDTO destination, string destMember, ResolutionContext context)
     => string.IsNullOrWhiteSpace(source.Product.PictureUrl) ? string.Empty :
        $"{configuration["BaseUrl"]}{source.Product.PictureUrl}";
}