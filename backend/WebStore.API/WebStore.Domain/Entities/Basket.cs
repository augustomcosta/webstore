﻿using WebStore.Domain.Entities.Base;

namespace WebStore.Domain.Entities;

public sealed class Basket : BaseEntity
{
    public Basket()
    {
    }

    public Basket(Guid id) : base(id)
    {
    }

    public Guid DeliveryMethodId { get; set; }
    public string PaymentIntentId { get; set; }
    public decimal ShippingPrice { get; set; }
    public List<BasketItem> BasketItems { get; set; } = [];

    public decimal TotalPrice
    {
        get { return BasketItems.Sum(item => item.Price * item.Quantity + ShippingPrice); }
    }
}