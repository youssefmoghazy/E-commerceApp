using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Shared.SharedTransferObjects.Authentication;

namespace Presentation.Controllers;

public class AuthenticationController(IServicesManger servicesManger) : ApiController
{
    //[httpPost]
    // Login (RequestBody{string email , string password})
    // => UserResponce {string token , string email , string DisplayName }

    [HttpPost("login")]
    public async Task<ActionResult<UserResponce>> Login(LoginRequest request)
        => Ok(await servicesManger.AuthenticationService.LoginAsync(request));
    //[httpPost]
    // Register (RequestBody { string email , string UserName ,string Password, string DisplayName , string PhoneNumber })
    // => UserResponce {string token . string name , string DisplayName}

    //[httpGet]
    // CheckEmail (string email) => bool
    [HttpPost("register")]
    public async Task<ActionResult<UserResponce>> Register(RegisterRequest request)
        => Ok(await servicesManger.AuthenticationService.RegisterAsync(request));
    //TODO
    [HttpGet("emailexists")]
    public async Task<ActionResult<bool>> CheckEmailAsync (string  email)
            => Ok(await servicesManger.AuthenticationService.CkeckEmailAsync(email));

    // [Authorize]
    // [HttpGet]
    // GetCurrentUserAddress() => return AddressDTO
    [Authorize]
    [HttpGet("address")]
    public async Task<ActionResult<bool>> GetAddress ()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await servicesManger.AuthenticationService.GetUserAddressAsync(email!));
    }
    // [Authorize]
    // [httpPost]
    // UpdateCurrentUserAddress() => return AddressDTO
    [Authorize]
    [HttpPut("address")]
    public async Task<ActionResult<AddressDTO>> updateAddress (AddressDTO addressDTO)
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await servicesManger.AuthenticationService.UpdateAddressAsync(addressDTO, email!));
    }
    // [Authorize]
    // [HttpGet]
    // GetCurrentUser()
    // => UserResponce { string token , string email , string DisplayName }
    [Authorize]
    [HttpGet]
    public async Task<ActionResult<UserResponce>> GetCurrentUser()
    {
        var email = User.FindFirstValue(ClaimTypes.Email);
        return Ok(await servicesManger.AuthenticationService.GetUserByEmail(email!));
    }

}
