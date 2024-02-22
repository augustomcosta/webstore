using WebStore.Domain.Entities;

namespace WebStore.Domain.Pagination;

public class ProductsBrandFilter : QueryStringParams
{
    public string? BrandName { get; set; }
    
}