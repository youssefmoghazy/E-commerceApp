using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Domain.Models.Product;
using Shared.SharedTransferObjects.Product;

namespace Services.Spicifications
{
    public class ProdectWithBrandAndTypeSpicifcations
        : BaseSpefications<Product>
    {
        // Use this CTOP to Create Quary to get product by id
        public ProdectWithBrandAndTypeSpicifcations(int id) 
            : base(product => product.Id == id)
        {
            addInclude(p => p.productBrand);
            addInclude(p => p.ProductType);
        }
        //Use this CTOP to create quary to get all product
        public ProdectWithBrandAndTypeSpicifcations(productQuaryParameters parameters)
            :base(product => 
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) && 
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.search) ||
            product.Name.ToLower().Contains(parameters.search.ToLower())))
        {
            addInclude(p => p.productBrand);
            addInclude(p => p.ProductType);
            applySorting(parameters.Sort);
            ApplyPagination(parameters.PageSize, parameters.PageIndex);
        }

        private void applySorting (ProrductSortingOptions options)
        {
            switch (options)
            {
                case ProrductSortingOptions.NameAsc:
                    addOrderBy(p => p.Name);
                    break;
                case ProrductSortingOptions.NameDesc:
                    addOrderyDescending(p => p.Name);
                    break;
                case ProrductSortingOptions.PriceAsc:
                    addOrderBy(p => p.Price);
                    break;
                case ProrductSortingOptions.PriceDesc:
                    addOrderyDescending(p => p.Price);
                    break;
                default:
                    break;
            }
        }
    }
}
