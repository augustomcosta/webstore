﻿using Newtonsoft.Json;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.Domain.Entities;

public sealed class Basket
{
    public Basket()
    {
        CreatedAt = DateTime.Now;
    }

    public string? Id { get; set; } = Guid.NewGuid().ToString();
    public string? UserId { get; set; }
    public string? DeliveryMethodId { get; set; }
    public double ShippingPrice { get; set; }
    public DateTime? CreatedAt {get; set;}

    [JsonIgnore]
    public User? User {get; set;}
    public DeliveryMethod? DeliveryMethod {get; set;}
    public List<BasketItem> BasketItems { get; set; } = [];
    private double _totalPrice;

    public double TotalPrice
    {
        get {return _totalPrice;}
        set {_totalPrice = value;} 
    }

   
    public void UpdateTotalPrice(){
        _totalPrice = BasketItems.Sum(item => item.Price * item.Quantity + ShippingPrice);
    }

    public void UpdateBasket(Basket basket)
    {
        basket.DeliveryMethodId = DeliveryMethodId;
        basket.ShippingPrice = ShippingPrice;
        basket.BasketItems = BasketItems;
        basket.TotalPrice = TotalPrice;
    }
}