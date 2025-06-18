using AutoMapper;
using Domain.Contracts;
using Domain.Exceptions;
using Domain.Models.Product;
using Services.Spicifications;
using ServicesAbstractions;
using Shared.SharedTransferObjects;
using Shared.SharedTransferObjects.Product;

namespace Services;

public class ProductService(IUnitOFWork unitOFWork ,IMapper mapper)
    : IProductService
{

    public async Task<PaginatedResponce<ProductResponce>> GetProductsAsync(productQuaryParameters quaryParameters)
    {
        var repository = unitOFWork.GetReposistory<Product,int>();
        var specification = new ProdectWithBrandAndTypeSpicifcations(quaryParameters);

        var totalConut = await unitOFWork.GetReposistory<Product,int>().CountAsync(new ProductCountSpecifications(quaryParameters));

        var products = await repository.GetAllAsynce(specification);
        var data = mapper.Map<IEnumerable<ProductResponce>>(products);
        var PageCount = data.Count();
        return new(quaryParameters.PageIndex, PageCount, totalConut,data);
    }
    public async Task<ProductResponce> GetProductAsync(int id)
    {
        var repository = unitOFWork.GetReposistory<Product,int>();
        var specification = new ProdectWithBrandAndTypeSpicifcations(id);
        var product = await repository.GetAsynce(specification) ??
            throw new ProductNotfoundException(id);
        return mapper.Map<ProductResponce>(product);
    }

    public async Task<IEnumerable<BrandResponce>> GetBrandsAsync()
    {
        var repository = unitOFWork.GetReposistory<ProductBrand, int>();
        var brands = await repository.GetAllAsynce();
        return mapper.Map<IEnumerable<ProductBrand>, IEnumerable<BrandResponce>>(brands);
    }

    public async Task<IEnumerable<TypeResponce>> GetTypesAsync()
    {
        var repository = unitOFWork.GetReposistory<ProductType, int>();
        var types = await repository.GetAllAsynce();
        return mapper.Map<IEnumerable<ProductType>,IEnumerable<TypeResponce>>(types);
    }

}
