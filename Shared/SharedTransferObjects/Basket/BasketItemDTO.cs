using System.ComponentModel.DataAnnotations;

namespace Shared.SharedTransferObjects.Basket;

public record BasketItemDTO
{
    public int id { get; set; }
    public string Productname { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Brand { get; set; } = default!;
    public string Type { get; set; } = default!;
}