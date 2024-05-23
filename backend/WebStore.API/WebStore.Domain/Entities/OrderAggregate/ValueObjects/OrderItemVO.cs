using System.ComponentModel.DataAnnotations.Schema;

namespace WebStore.Domain.Entities.OrderAggregate.ValueObjects;


[ComplexType]
public class OrderItemVO
{
    public string Id { get; set; }
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string ProductName { get; set; }
    public string ProductImgUrl { get; set; }
    public string Brand { get; set; }
    public string Category { get; set; }

    public OrderItemVO()
    {
    }
 
    public OrderItemVO(
        string id,
        int quantity,
        double price,
        string productName,
        string productImgUrl,
        string brand,
        string category)
    {
        Id = id;
        Quantity = quantity;
        Price = price;
        ProductName = productName;
        ProductImgUrl = productImgUrl;
        Brand = brand;
        Category = category;
    }
}