namespace Services.Profiles;

public class UserProfile :Profile
{
    public UserProfile()
    {
        CreateMap<AddressDTO,Address>().ReverseMap();
    }
}
