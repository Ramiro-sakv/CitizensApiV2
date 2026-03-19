using CitizensApiV2.DTOs;
using CitizensApiV2.Services;
using Microsoft.AspNetCore.Mvc;

namespace CitizensApiV2.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CitizenController : ControllerBase
{
    private readonly CitizenService _service;

    public CitizenController(CitizenService service)
    {
        _service = service;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("{ci}")]
    public IActionResult GetByCI(string ci)
    {
        try
        {
            var citizen = _service.GetByCI(ci);
            return Ok(citizen);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateCitizenDto dto)
    {
        try
        {
            var citizen = await _service.Create(dto);
            return Ok(citizen);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{ci}")]
    public IActionResult Update(string ci, UpdateCitizenDto dto)
    {
        try
        {
            var citizen = _service.Update(ci, dto);
            return Ok(citizen);
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete("{ci}")]
    public IActionResult Delete(string ci)
    {
        try
        {
            _service.Delete(ci);
            return Ok("Citizen deleted");
        }
        catch (Exception ex)
        {
            return NotFound(ex.Message);
        }
    }
}