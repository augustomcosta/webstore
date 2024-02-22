namespace WebStore.Domain.Pagination;

public class OrdersDateFilter : QueryStringParams
{
    public DateTime OrderDate { get; set; }
}