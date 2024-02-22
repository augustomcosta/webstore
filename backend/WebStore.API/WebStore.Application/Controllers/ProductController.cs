using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;
using WebStore.Domain.Entities;
using WebStore.Domain.Pagination;

namespace WebStore.API.Controllers;


[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _service;
    
    public ProductController(IProductService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var products = await _service.GetAll();

        if (products == null)
        {
            return NotFound("No products were found");
        }

        return Ok(products);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]ProductDto product)
    {
        await _service.Create(product);
        return Ok(product);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid? id)
    {
        var product = await _service.GetById(id);

        if (product == null)
        {
            throw new Exception($"Product with Id {id} not found.");
        }
        
        return Ok(product);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, ProductDto product)
    {
        await _service.Update(id, product);
        return Ok(product);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid? id)
    {
        await _service.Delete(id);
        return Ok();
    }
    
    [HttpGet("pagination")]
    public async Task<IActionResult> GetWithPagination([FromQuery]ProductParams productParams)
    {
        var products = await _service.GetWithPagination(productParams);

        var metadata = new
        {
            products.TotalCount,
            products.PageSize,
            products.CurrentPage,
            products.TotalPages,
            products.HasNext,
            products.HasPrevious
        };

        Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(metadata));
        return Ok(products);
    }

    [HttpGet("filter/price/pagination")]
    public async Task<IActionResult> GetWithPriceFilter([FromQuery] ProductsPriceFilter priceFilter)
    {
        var products = await _service.GetWithPriceFilter(priceFilter);
        
        var metadata = new
        {
            products.TotalCount,
            products.PageSize,
            products.CurrentPage,
            products.TotalPages,
            products.HasNext,
            products.HasPrevious
        };

        Response.Headers.Append("X-pagination", JsonConvert.SerializeObject(metadata));
        return Ok(products);
    }

    [HttpGet("filter/brand/pagination")]
    public async Task<IActionResult> GetProductsByBrandNameAsync([FromQuery]QueryStringParams query, [FromQuery]string brandName)
    {
        var products = await _service.GetProductsByBrandNameAsync(query,brandName);
        return Ok(products);
    }

    [HttpGet("filter/category/pagination")]
    public async Task<IActionResult> GetProductsByCategoryNameAsync([FromQuery] QueryStringParams query,
        [FromQuery] string categoryName)
    {
        var products = await _service.GetProductsByCategoryNameAsync(query, categoryName);
        return Ok(products);
    }
}