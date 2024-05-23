using System.Runtime.Serialization;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.DTOs.OrderDtoAggregate;

[DataContract]
public class OrderDto
{
    public OrderDto(Guid id, double subTotal, string buyerEmail, List<OrderItemVoDto> orderItems, int totalItemQuantity,
        string orderDate, DeliveryMethod deliveryMethod, AddressVO shippingAddress, string deliveryMethodId,
        double total, string userId)
    {
        this.id = id;
        this.subTotal = subTotal;
        this.buyerEmail = buyerEmail;
        this.orderItems = orderItems;
        this.totalItemQuantity = totalItemQuantity;
        this.orderDate = orderDate;
        this.deliveryMethod = deliveryMethod;
        this.shippingAddress = shippingAddress;
        this.deliveryMethodId = deliveryMethodId;
        this.total = total;
        this.userId = userId;
    }

    public OrderDto()
    {
        
    }
    public Guid id { get; set; }
    public double subTotal { get; set; }
    public string buyerEmail { get; set; }
    public List<OrderItemVoDto> orderItems { get; set; }
    
    public int? totalItemQuantity { get; set; }
    public string orderDate { get; set; }
    public DeliveryMethod deliveryMethod { get; set; }
    public AddressVO shippingAddress { get; set; }
    public string deliveryMethodId { get; set; }
    public double total { get; set; }
    public string userId { get; set; }
    
    
}
