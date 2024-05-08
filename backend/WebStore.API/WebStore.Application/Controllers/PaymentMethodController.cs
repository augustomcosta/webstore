using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;

namespace WebStore.API.Controllers;

[EnableCors("AllowClient")]
[Route("api/[controller]")]
[ApiController]
public class PaymentMethodController : Controller
{
    private readonly IPaymentMethodService<PaymentMethodDto> _service;
    
    public PaymentMethodController(IPaymentMethodService<PaymentMethodDto> service)
    {
        _service = service;
    }

    [HttpGet("get-payment-methods")]
    public async Task<IActionResult> GetAll()
    {
        var paymentMethods = await _service.GetAll();
        
        return Ok(paymentMethods);
    }
    
}