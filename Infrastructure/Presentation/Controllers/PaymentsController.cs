using Microsoft.AspNetCore.Authorization;
using Shared.SharedTransferObjects.Authentication;

namespace Presentation.Controllers;
using Shared.SharedTransferObjects.Billing;
public class PaymentsController(IServicesManger services)
    :ApiController
{
    [HttpPost("{basketId}")]
    public async Task<ActionResult<BasketDTO>> CreateOrUpdate(string basketId)
    {
        return Ok(await services.paymentService.CreateOrUpdatePaymentIntent(basketId));
    }
    [Authorize]
    [HttpGet("billing/{basketId}/{orderId}")]
    public async Task<ActionResult<BillingDIO>> CreateBilling(string basketId, string orderId)
    {
        return Ok(await services.BillingService.CreateBilling(basketId, GetEmailFromToken(), orderId));
    }

    [Authorize]
    [HttpGet("check-payment-status/{referenceNumber}")]
    public async Task<IActionResult> CheckPaymentStatus(string referenceNumber)
    {
        return Ok(await services.BillingService.GetBillingStatus(referenceNumber));
    }

}
