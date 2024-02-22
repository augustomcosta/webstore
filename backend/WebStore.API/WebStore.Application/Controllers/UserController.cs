using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.Interfaces;

namespace WebStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _service;
    
    public UserController(IUserService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var users = await _service.GetAll();
        if (users == null)
        {
            throw new Exception("No users were found");
        }
        return Ok(users);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody]UserDto user)
    {
        await _service.Create(user);
        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var user = await _service.GetById(id);
        if (user == null)
        {
            throw new Exception($"User with Id {id} not found");
        }
        return Ok(user);
    }

    [HttpPut("{int:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody]UserDto user)
    {
        await _service.Update(id, user);
        return Ok(user);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _service.Delete(id);
        return Ok();
    }
}