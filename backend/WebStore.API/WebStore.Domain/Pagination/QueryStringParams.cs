using System.ComponentModel.DataAnnotations;

namespace WebStore.Domain.Pagination;

public class QueryStringParams
{
    private const int maxPageSize = 50;
    public int PageNumber { get; set; } = 1;
    
    [Range(1,50)]
    public int PageSize { get; set; } = 1;
}