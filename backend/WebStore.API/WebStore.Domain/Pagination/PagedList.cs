namespace WebStore.Domain.Pagination;

public class PagedList<T> : List<T> where T : class
{
    public int CurrentPage { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int TotalCount { get; set; }

    public bool HasPrevious => CurrentPage > 1;
    public bool HasNext => CurrentPage < TotalPages;

    public PagedList() {}
    
    public PagedList(List<T> items, int currentPage, int totalPages, int totalCount)
    {
        TotalCount = totalCount;
        CurrentPage = currentPage;
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        AddRange(items);
    }

    public static PagedList<T> ToPagedList(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var totalCount = source.Count();
        var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
        return new PagedList<T>(items, totalCount, pageNumber, pageSize);
    }
}