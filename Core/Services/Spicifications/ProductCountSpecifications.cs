using System.Linq.Expressions;
using Domain.Models.Product;
using Shared.SharedTransferObjects.Product;

namespace Services.Spicifications
{
    internal class ProductCountSpecifications(productQuaryParameters parameters)
                : BaseSpefications<Product>(CreateCriteria(parameters))
    {
        public static Expression<Func<Product, bool>> CreateCriteria(productQuaryParameters parameters)
        {
            return product =>
            (!parameters.BrandId.HasValue || product.BrandId == parameters.BrandId) && 
            (!parameters.TypeId.HasValue || product.TypeId == parameters.TypeId) &&
            (string.IsNullOrWhiteSpace(parameters.search) ||
            product.Name.ToLower().Contains(parameters.search.ToLower()));
        }
    }
}
