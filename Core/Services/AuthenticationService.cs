using Microsoft.EntityFrameworkCore;

namespace Services;

public class AuthenticationService(UserManager<ApplicationUser> userManager,
    IOptions<JWTOptions> options,IMapper mapper)
    : IAuthenticationService
{
    public async Task<UserResponce> LoginAsync(LoginRequest request)
    {
        var user = await userManager.FindByEmailAsync(request.Email)
            ?? throw new UserNotFoundExeption(request.Email);
        var IsValid = await userManager.CheckPasswordAsync(user, request.password);
        if (IsValid)
            return new(request.Email, user.DisplayName, await CreateJWTAsync(user));
        throw new UnauthorizedException();
    }

    public async Task<UserResponce> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser
        {
            Email = request.Email,
            DisplayName = request.DisplayName,
            PhoneNumber = request.PhoneNumber,
            UserName = request.UserName,
        };
        var result = await userManager.CreateAsync(user,request.Password);
        if (result.Succeeded)
            return new(request.Email, user.DisplayName, await CreateJWTAsync(user));
        var errors = result.Errors.Select(e => e.Description).ToList<string>();
        throw new BadRequestException(errors);
           
    }

    public async Task<bool> CkeckEmailAsync(string Email)
        => (await userManager.FindByEmailAsync(Email)) != null;

    public async Task<AddressDTO> GetUserAddressAsync(string Email)
    {
        var user = await userManager.Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Email == Email)
            ?? throw new UserNotFoundExeption(Email);
        //if (user.Address is not null)
        //throw new AddressNotFoundException(user.UserName!);
            return mapper.Map<AddressDTO>(user.Address);
    }
    public async Task<AddressDTO> UpdateAddressAsync(AddressDTO addressDTO, string Email)
    {
        var user = await userManager.Users.Include(u => u.Address)
            .FirstOrDefaultAsync(u => u.Email == Email)
            ?? throw new UserNotFoundExeption(Email);
        if(user.Address is not null) // this for update
        {
            user.Address.FirstName = addressDTO.FirstName;
            user.Address.LastName = addressDTO.LastName;
            user.Address.City = addressDTO.city;
            user.Address.Country = addressDTO.Country;
            user.Address.street = addressDTO.street;
        }
        else // this for create
        {
            user.Address = mapper.Map<Address>(addressDTO);
        }
        await userManager.UpdateAsync(user);

        return mapper.Map<AddressDTO>(user.Address);
    }

    public async Task<UserResponce> GetUserByEmail(string Email)
    {
        var user = await userManager.FindByEmailAsync(Email)
            ?? throw new UserNotFoundExeption(Email);
        return new(Email, user.DisplayName , await CreateJWTAsync(user));
    }
    private async Task<string> CreateJWTAsync (ApplicationUser user)
    {
        var JWT = options.Value;
        var claims = new List<Claim>()
        {
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Name, user.UserName!),

        };
        var roles = await userManager.GetRolesAsync(user);
        foreach (var role in roles)
            claims.Add(new(ClaimTypes.Role, role));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWT.SecretKey));
        var cred = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: JWT.Issuer,
            audience: JWT.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddDays(JWT.DurationInDays),
            signingCredentials: cred
            ); 
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.WriteToken(token);
    }
}
