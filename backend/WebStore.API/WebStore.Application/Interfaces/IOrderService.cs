using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.ValueObjects;

namespace WebStore.API.Interfaces;

public interface IOrderService : IBaseService<OrderDto>
{
    Task<OrderDto> CreateOrder(string basketId, string userId);
    Task AddItemToOrder(Guid id, OrderItemVO orderItem);
}