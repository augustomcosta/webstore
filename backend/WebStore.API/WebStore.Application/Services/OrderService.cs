﻿using AutoMapper;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;
using WebStore.Domain.Repositories;

namespace WebStore.API.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;
    private readonly IMapper _mapper;

    public OrderService(IOrderRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }
    public async Task<IEnumerable<OrderDto>> GetAll()
    {
        var orders = await _repository.GetAll();
        return _mapper.Map<IEnumerable<OrderDto>>(orders);
    }

    public async Task<OrderDto> GetById(Guid? id)
    {
        var orderById = await _repository.GetById(id);
        return _mapper.Map<OrderDto>(orderById);
    }

    public async Task<OrderDto> Create(OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _repository.Create(order);
        return orderDto;
    }

    public async Task Update(Guid? id, OrderDto orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _repository.Update(id, order);
    }

    public async Task Delete(Guid? id)
    {
        await _repository.Delete(id);
    }

    public async Task AddItemToOrder(Guid id, OrderDto orderDto, OrderItemVO orderItem)
    {
        var order = _mapper.Map<Order>(orderDto);
        await _repository.AddItemToOrder(id,order,orderItem);
    }
}