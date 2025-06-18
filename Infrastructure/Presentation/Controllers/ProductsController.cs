using Microsoft.AspNetCore.Authorization;
using Presentation.Attributes;

namespace Presentation.Controllers;

public class ProductsController(IServicesManger servicermanger) : ApiController
{
    // Get all prodects
    [RedisCache]
    [HttpGet]
    public async Task<ActionResult<PaginatedResponce<ProductResponce>>> GetAllProdects([FromQuery]productQuaryParameters quaryParameters) // Get BaseURl/api/Prodects
    {
        var prodects = await servicermanger.ProductService.GetProductsAsync(quaryParameters);
        return Ok(prodects);
    }
    // Get prodect
    [HttpGet("{id}")]
    public async Task<ActionResult<ProductResponce>> GetProdect(int id) // Get BaseURl/api/Prodects/{id}
    {
        var prodect = await servicermanger.ProductService.GetProductAsync(id);
        return Ok(prodect);
    }
    // Get all brands
    [HttpGet("brands")]
    public async Task<ActionResult<IEnumerable<BrandResponce>>> GetAllBrands() // Get BaseURl/api/Prodects/brands
    {
        var Brands = await servicermanger.ProductService.GetBrandsAsync();
        return Ok(Brands);
    }
    // Get all types
    [HttpGet("types")]
    public async Task<ActionResult<IEnumerable<TypeResponce>>> GetAllTypes() // Get BaseURl/api/Prodects/types
    {
        var types = await servicermanger.ProductService.GetTypesAsync();
        return Ok(types);
    }
}
