﻿using WebStore.API.DTOs;
using WebStore.API.DTOs.OrderDtoAggregate;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.Interfaces;

public interface IOrderService : IBaseService<OrderDto>
{
    Task<OrderDto> CreateOrder(string basketId, string userId, string deliveryMethodId);
    Task AddItemToOrder(Guid id, OrderItemVO orderItem);
    Task<List<OrderDto>> GetAllOrdersForUser(string userId);
}