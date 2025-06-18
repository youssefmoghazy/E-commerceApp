namespace Presentation.Controllers;

public class PaymentsController(IServicesManger services)
    :ApiController
{
    [HttpPost("{basketId}")]
    public async Task<ActionResult<BasketDTO>> CreateOrUpdate(string basketId)
    {
        return Ok(await services.paymentService.CreateOrUpdatePaymentIntent(basketId));
    }

}
