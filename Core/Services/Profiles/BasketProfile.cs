namespace Services.Profiles;

public class BasketProfile : Profile
{
    public BasketProfile()
    {
        CreateMap<CustomerBasket, BasketDTO>()
            .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.BasketItems))
            .ReverseMap()
            .ForMember(dest => dest.BasketItems, opt => opt.MapFrom(src => src.Items));

        CreateMap<BasketItem, BasketItemDTO>().ReverseMap();
    }
}

