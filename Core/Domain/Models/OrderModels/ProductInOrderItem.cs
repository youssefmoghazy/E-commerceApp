namespace Domain.Models.OrderModels;

public class ProductInOrderItem
{
    public ProductInOrderItem()
    {
         
    }
    public ProductInOrderItem(int productid, string productname, string pictureUrl)
    {
        Productid = productid;
        Productname = productname;
        PictureUrl = pictureUrl;
    }

    public int Productid { get; set; }
    public string Productname { get; set; } = default!;
    public string PictureUrl { get; set; } = default!;
}