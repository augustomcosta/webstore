using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.DTOs.OrderDtoAggregate;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities.OrderAggregate;
using WebStore.Domain.Entities.OrderAggregate.ValueObjects;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;
    private readonly IMapper _mapper;

    public OrderController(IOrderService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();
        if (orders == null) return NotFound("No orders were found.");
        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetById(id);
        if (order == null) return NotFound($"Order with Id {id} not found.");
        return Ok(order);
    }

    [HttpPost]
    public async Task<IActionResult> Create(OrderDto order)
    {
        await _service.Create(order);
        return Ok(order);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, OrderDto order)
    {
        await _service.Update(id, order);
        return Ok(order);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }

    [HttpPut("add-item")]
    public async Task<IActionResult> AddItemToOrder([FromQuery] Guid id, OrderItemVO orderItem)
    {
        await _service.AddItemToOrder(id, orderItem);
        return Ok();
    }

    [HttpPost("create-order")]
    public async Task<IActionResult> CreateOrder([FromQuery] string basketId, string userId,string deliveryMethodId)
    {
        var order = await _service.CreateOrder(basketId, userId, deliveryMethodId);
        if (order is null) throw new Exception("Error while creating order");

        return Ok(order);
    }

    [HttpGet("get-all-orders-for-user")]
    public async Task<IActionResult> GetAllOrdersForUser(string userId)
    {
        var orders = await _service.GetAllOrdersForUser(userId);
        
        return Ok(orders);
    }
}