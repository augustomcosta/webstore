﻿using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Repositories.Base;

namespace WebStore.Domain.Repositories;

public interface IOrderRepository : IBaseRepository<Order>
{
    Task<Order> AddItemToOrder(Guid id, Order order, OrderItemVO orderItem);
}