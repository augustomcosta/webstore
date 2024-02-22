namespace WebStore.Domain.Pagination;

public class ProductsPriceFilter : QueryStringParams
{
    public decimal? Price { get; set; }
    public string? PriceCriteria { get; set; }
}