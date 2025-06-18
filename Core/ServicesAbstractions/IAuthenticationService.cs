using System.ComponentModel.DataAnnotations;
using Shared.SharedTransferObjects.Authentication;

namespace ServicesAbstractions;

public interface IAuthenticationService
{
    //[httpPost]
    // Login (RequestBody{string email , string password})
    // => UserResponce {string token , string email , string DisplayName }
    Task<UserResponce> LoginAsync(LoginRequest request);
    //[httpPost]
    // Register (RequestBody { string email , string UserName ,string Password, string DisplayName , string PhoneNumber })
    // => UserResponce {string token . string name , string DisplayName}
    Task<UserResponce> RegisterAsync(RegisterRequest request);

    Task<bool> CkeckEmailAsync(string Email);
    Task<AddressDTO> GetUserAddressAsync(string Email);
    Task<AddressDTO> UpdateAddressAsync (AddressDTO addressDTO , string Email);
    Task<UserResponce> GetUserByEmail(string Email);
}
