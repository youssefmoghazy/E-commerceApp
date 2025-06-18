namespace Domain.Models.Product;

public class Product : BaseEntity<int>
{
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; } = decimal.Zero;
    public ProductBrand productBrand { get; set; } = default!;
    public int BrandId { get; set; }
    public ProductType ProductType { get; set; } = default!;
    public int TypeId { get; set; }
}
