using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;

namespace WebStore.API.Interfaces;

public interface IOrderService : IBaseService<OrderDto>
{
    Task AddItemToOrder(Guid id, OrderDto orderDto, OrderItemVO orderItem);
}