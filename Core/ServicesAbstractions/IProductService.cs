using Shared.SharedTransferObjects;
using Shared.SharedTransferObjects.Product;

namespace ServicesAbstractions;

public interface IProductService
{
    // get all products => IEunmerable<ProductResponce>
    Task<PaginatedResponce<ProductResponce>> GetProductsAsync(productQuaryParameters quaryParameters);
    // get product => ProductResponce
    Task<ProductResponce> GetProductAsync(int id);
    // get brand => BrandResponce
    Task<IEnumerable<BrandResponce>> GetBrandsAsync();
    // get type => TypeResponce
    Task<IEnumerable<TypeResponce>> GetTypesAsync();

}
