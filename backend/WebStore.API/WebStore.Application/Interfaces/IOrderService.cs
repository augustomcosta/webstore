using WebStore.API.DTOs;
using WebStore.API.Interfaces.Base;
using WebStore.Domain.Entities.OrderAggregate;

namespace WebStore.API.Interfaces;

public interface IOrderService : IBaseService<OrderDto> {}