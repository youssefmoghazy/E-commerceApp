
namespace Presentation.Controllers;

public class basketsController(IServicesManger servicesManger) : ApiController
{
    // get Basket by id
    [HttpGet]
    public async Task<ActionResult<BasketDTO>> Get(string id)
    {
        var basket = await servicesManger.BasketServices.GetAsync(id);
        return Ok(basket);
    }
    // update basket (BasketDTO) => create basket , add item , remove item
    [HttpPost]
    public async Task<ActionResult<BasketDTO>> Update(BasketDTO basket)
    {
        var Updatedbasket = await servicesManger.BasketServices.UpdateAsync(basket);
        return Ok(Updatedbasket);
    }
    // Delete basket
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Detete(string id)
    {
        await servicesManger.BasketServices.DeleteAsync(id);
        return NoContent(); //204 no content 
    }

}
