namespace WebStore.Domain.Pagination;

public class OrdersEmailFilter : QueryStringParams
{
    public string BuyerEmail { get; set; } = "";
}