using Microsoft.AspNetCore.Authorization;
using Shared.SharedTransferObjects.Orders;

namespace Presentation.Controllers;

[Authorize]
public class OrdersController(IServicesManger service)
    : ApiController
{
    /// Create (ShipToAddress , basketId , DeliverMethodId )
    [HttpPost]
    public async Task<ActionResult<OrderResponce>> CreateAsync (OrderRequest request)
    {

        return Ok(await service.OrderService.CreateAsync(request, GetEmailFromToken()));
    }
    /// GetAll
    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderResponce>>> GetAll()
    {
        return Ok(await service.OrderService.GetAllAsync(GetEmailFromToken()));
    }
    /// Get
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<OrderResponce>> Get(Guid id)
    {
        return Ok(await service.OrderService.GetAsync(id));
    }
    /// Get DileverMethod 
    [HttpGet("deliveryMethods"),AllowAnonymous]
    public async Task<ActionResult<IEnumerable<DeliverymethodResponce>>> Get()
    {
        return Ok(await service.OrderService.GetDeliveryMethodAsync());
    }

}
