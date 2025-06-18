global using Microsoft.AspNetCore.Mvc;
global using ServicesAbstractions;
global using Shared.SharedTransferObjects.Basket;
global using Shared.SharedTransferObjects;
global using Shared.SharedTransferObjects.Product;
using System.Security.Claims;

namespace Presentation.Controllers;
[Route("api/[controller]")]
[ApiController]
public abstract class ApiController : ControllerBase
{
    protected string GetEmailFromToken() => User.FindFirstValue(ClaimTypes.Email)!;
}
