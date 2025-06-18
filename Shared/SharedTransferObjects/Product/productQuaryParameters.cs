namespace Shared.SharedTransferObjects.Product;

public class productQuaryParameters
{
    private const int defaultPageSize = 5;
    private const int maxPageSize = 10;
    public int? BrandId { get; set; }
    public int? TypeId { get; set; }
    public ProrductSortingOptions Sort { get; set; }
    public string? search {  get; set; }
    private int _PageSize { get; set; } = defaultPageSize;
    public int PageIndex { get; set; } = 1;

    public int PageSize
    {
        get => _PageSize;
        set => _PageSize = value > 0 && value < maxPageSize ? value : defaultPageSize;
    }
}
