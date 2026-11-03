using EquipIT.Api.Data;
using EquipIT.Api.Models;
using EquipIT.Api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace EquipIT.Api.Controllers;

[ApiController]
[Route("api/equipment")]
public class EquipmentController : ControllerBase
{

   private readonly IEquipmentService _service;
     EquipmentController(IEquipmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(_service.GetAllEquipmentAsync());
    }

    [HttpPut("{id:int}/borrow")]
    public async Task<IActionResult> Borrow(int id, string FirstName, string LastName)
    {
        if (id <= 0)
        {
            await Task.FromException<IEnumerable<Equipment>>(new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero."));
        }
        if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName))
        {
            await Task.FromException<IEnumerable<Equipment>>(new ArgumentNullException("FirstName and LastName must be provided."));

        }
       

       return Ok( _service.BorrowEquipmentAsync(id, FirstName, LastName));
    }

    [HttpPut("{id:int}/return")]
    public async Task<IActionResult> Return(int id)
    {
        if (id <= 0)
        {
            await Task.FromException<IEnumerable<Equipment>>(new ArgumentOutOfRangeException(nameof(id), "Id must be greater than zero."));
        }
       
        return Ok(_service.ReturnEquipmentAsync(id));


    }
}
