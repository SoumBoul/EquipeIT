using EquipIT.Api.Data;
using EquipIT.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EquipIT.Api.Services;

public class EquipmentService : IEquipmentService
{
    private readonly EquipITDbContext _context;

    public EquipmentService(EquipITDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync()
    {
        if (_context.Equipments == null)
        {
            throw new InvalidOperationException("No equipment found.");
        }
        if (_context.Equipments.Count() == 0)
        {
            throw new InvalidOperationException("No equipment found.");
        }
        return await _context.Equipments.ToListAsync();
    }


    public async Task BorrowEquipmentAsync([Range(1, int.MaxValue)] int id , string firstName, string lastName)
    {
        

        var equipment = await _context.Equipments.FindAsync(id);

       if(equipment.Id == null)
        {
            throw new InvalidOperationException($"Equipment with id {id} was not found.");
        }
        if (!equipment.IsAvailable)
        {
           throw new InvalidOperationException("Ce matériel est déjà emprunté.");
        }

       
        equipment.IsAvailable = false;
        equipment.BorrowerFirstName = firstName;
        equipment.BorrowerLastName = lastName;
        equipment.BorrowDate = DateTime.UtcNow;

       await _context.SaveChangesAsync();
    }

    public async Task ReturnEquipmentAsync([Range(1, int.MaxValue)] int id)
    {
        // TODO: Implement business logic by checking availability and saving via EF Core
        var equipment = await _context.Equipments.FindAsync(id);
       
        if(equipment==null)
        {
            throw new InvalidOperationException($"Equipment with id {id} was not returned.");
        }
       

        if (equipment.IsAvailable != false)
      
        {
            throw new InvalidOperationException($"Equipment with id {id} is returned.");
        }

        equipment.LastReturnDate = DateTime.UtcNow;

        equipment.IsAvailable = true;
        equipment.BorrowerFirstName = null;
        equipment.BorrowerLastName = null;
        equipment.BorrowDate = null;
        await _context.SaveChangesAsync();
    }

    public async Task<Equipment> FindEquipmentAsync(int id)
    {
        var equipment = await _context.Equipments.FindAsync(id);
        if (equipment == null)
        {
            throw new InvalidOperationException($"Equipment with id {id} was not found.");
        }

        else
             throw new InvalidOperationException($"Equipment with id {id} is  found.");
            
          

        
    }
}
