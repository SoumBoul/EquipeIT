using EquipIT.Api.Data;
using EquipIT.Api.Models;
using EquipIT.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;


namespace EquipIT.Api.Controllers;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{

    private readonly IEquipmentService _service;
    public EquipmentController(IEquipmentService service)
    {
        _service = service;
    }


    [ProducesResponseType(typeof(IEnumerable<Equipment>), 200)]
    [ProducesResponseType(typeof(IEnumerable<Equipment>), 400)]
    [HttpGet("All")]

    public async Task<IActionResult> GetAll()
    {
        if (_service == null)
        {
            return BadRequest("Service is not available.");
        }
        return Ok(await _service.GetAllEquipmentAsync());
    }

    [ProducesResponseType(typeof(IEnumerable<Equipment>), statusCode: 200)]
    [ProducesResponseType(typeof(IEnumerable<Equipment>), statusCode: 204)]
    [ProducesResponseType(typeof(IEnumerable<Equipment>), statusCode: 400)]
    [HttpPut("{id:int}/borrow")]
    public async Task<IActionResult> Borrow([Range(1, int.MaxValue)]int id, [FromQuery, Required] string firstName, [FromQuery, Required] string lastName)
    {
       

        await _service.BorrowEquipmentAsync(id, firstName, lastName);
        return NoContent();
    }

    [ProducesResponseType(typeof(IEnumerable<Equipment>), 200)]
    [ProducesResponseType(typeof(IEnumerable<Equipment>), 400)]

    [HttpPut("{id:int}/return")]
    public async Task<IActionResult> Return([Range(1, int.MaxValue)] int id, [FromQuery, Required] string firstName, [FromQuery, Required] string lastName)
    {
        await _service.ReturnEquipmentAsync(id);

        return Ok("Service returned with succes");




    }

}