﻿namespace WebStore.Domain.Entities;

public class BasketItem
{
    public string Id { get; set; } = "";
    public int Quantity { get; set; }
    public string ProductName { get; set; } = "";
    public string ProductImgUrl { get; set; } = "";
    public double Price { get; set; }
    public string Brand {get; set;} = "";
    public string Category {get; set;} = "";
}