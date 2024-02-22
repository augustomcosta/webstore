using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BrandController : ControllerBase
{
    private readonly IBrandService _service;
    
    public BrandController(IBrandService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var brands = await _service.GetAll();
        return Ok(brands);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid? id)
    {
        var brandById = await _service.GetById(id);
        if (brandById == null)
        {
            throw new Exception($"Brand with Id {id} not found.");
        }
        return Ok(brandById);
    }

    [HttpPost]
    public async Task<IActionResult> Create(BrandDto brand)
    {
        await _service.Create(brand);
        return Ok(brand);
    }

    [HttpPut]
    public async Task<IActionResult> Update(Guid? id, BrandDto brand)
    {
        await _service.Update(id, brand);
        return Ok(brand);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid? id)
    {
        await _service.Delete(id);
        return Ok();
    }

    [HttpGet("pagination")]
    public async Task<IActionResult> GetWithPagination(BrandParams brandParams)
    {
        var brands = await _service.GetWithPagination(brandParams);
        return Ok(brands);
    }
}