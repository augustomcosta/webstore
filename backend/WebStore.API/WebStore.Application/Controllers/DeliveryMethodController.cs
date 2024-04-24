using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;

namespace WebStore.API.Controllers;

[EnableCors("AllowClient")]
[Route("api/[controller]")]
[ApiController]
public class DeliveryMethodController : ControllerBase
{
    private readonly IDeliveryMethodService<DeliveryMethodDto> _service;
    
    public DeliveryMethodController(IDeliveryMethodService<DeliveryMethodDto> service)
    {
        _service = service;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var deliverymethods = await _service.GetAll();

        return Ok(deliverymethods);
    }

    [HttpPost]
    public async Task<IActionResult> Create(DeliveryMethodDto deliveryMethodDto)
    {
        await _service.Create(deliveryMethodDto);

        return Ok(deliveryMethodDto);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var deliveryMethod = await _service.GetById(id);
        return Ok(deliveryMethod);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}