using Newtonsoft.Json;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Domain.Entities;

public sealed class Basket
{
    public Basket()
    {
        CreatedAt = DateTime.Now;
    }

    public string? Id { get; set; }
    public string? UserId { get; set; }
    public Guid? DeliveryMethodId { get; set; }
    public string? PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
    public DateTime? CreatedAt {get; set;}

    [JsonIgnore]
    public User? User {get; set;}
    public DeliveryMethod? DeliveryMethod {get; set;}
    public List<BasketItem> BasketItems { get; set; } = new List<BasketItem>();
    private decimal _totalPrice;

    public decimal TotalPrice
    {
        get {return _totalPrice;}
        set {_totalPrice = value;} 
    }

   
    public void UpdateTotalPrice(){
        _totalPrice = BasketItems.Sum(item => item.Price * item.Quantity + ShippingPrice);
    }

    public void UpdateBasket(Basket basket)
    {
        basket.Id = Id;
        basket.UserId = UserId;
        basket.DeliveryMethodId = DeliveryMethodId;
        basket.PaymentIntentId = PaymentIntentId;
        basket.ShippingPrice = ShippingPrice;
        basket.BasketItems = BasketItems;
    }
}