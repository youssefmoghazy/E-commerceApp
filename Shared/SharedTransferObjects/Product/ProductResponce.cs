namespace Shared.SharedTransferObjects.Product;

public record ProductResponce
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Description { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; } = decimal.Zero;
    public string ProductBrand { get; set; } = default!;
    public string ProductType { get; set; } = default!;
}
