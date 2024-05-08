using Microsoft.AspNetCore.Mvc;
using WebStore.API.DTOs;
using WebStore.API.DTOs.UserDto;
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
    public async Task<IActionResult> GetById(string id)
    {
        var user = await _service.GetById(id);
        if (user == null)
        {
            throw new Exception($"User with Id {id} not found");
        }
        return Ok(user);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(string id, [FromBody]UserDto user)
    {
        await _service.Update(id, user);
        return Ok(user);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(string id)
    {
        await _service.Delete(id);
        return Ok();
    }

    [HttpPut("update-user-address")]
    public async Task<IActionResult> UpdateUserAddress([FromQuery]string id, [FromBody]AddressVoDto addressVoDto)
    {
        var updatedAddress = await _service.UpdateUserAddress(id, addressVoDto);
        return Ok(updatedAddress);
    }

    [HttpGet("get-user-address")]
    public async Task<IActionResult> GetUserAddress([FromQuery]string? id)
    {
        var address = await _service.GetUserAddress(id);
        return Ok(address);
    }
}