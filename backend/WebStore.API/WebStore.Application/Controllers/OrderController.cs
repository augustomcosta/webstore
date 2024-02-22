using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IOrderService _service;

    public OrderController(IOrderService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _service.GetAll();
        if (orders == null)
        {
            return NotFound("No orders were found.");
        }
        return Ok(orders);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var order = await _service.GetById(id);
        if (order == null)
        {
            return NotFound($"Order with Id {id} not found.");
        }
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
}