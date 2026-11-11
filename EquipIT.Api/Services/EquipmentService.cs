using EquipIT.Api.Data;
using EquipIT.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace EquipIT.Api.Services;

public class EquipmentService : IEquipmentService
{
    private readonly EquipITDbContext _context;

    public EquipmentService(EquipITDbContext context) => _context = context;

    public async Task<IEnumerable<Equipment>> GetAllEquipmentAsync() =>
        await _context.Equipments.AsNoTracking().OrderBy(e => e.Id).ToListAsync();


    public async Task BorrowEquipmentAsync(int id, string firstName, string lastName)
    {
        if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            throw new ArgumentException("Le prénom et le nom sont obligatoires.");

        var equipment = await FindEquipmentAsync(id);
        if (!equipment.IsAvailable)
            throw new InvalidOperationException("Ce matériel est déjà emprunté.");

        equipment.IsAvailable = false;
        equipment.BorrowerFirstName = firstName.Trim();
        equipment.BorrowerLastName = lastName.Trim();
        equipment.BorrowDate = DateTime.UtcNow;
        await _context.SaveChangesAsync();
    }

    public async Task ReturnEquipmentAsync(int id)
    {
        var equipment = await FindEquipmentAsync(id);
        if (equipment.IsAvailable)
            throw new InvalidOperationException("Ce matériel est déjà disponible.");

        equipment.LastReturnDate = DateTime.UtcNow;
        equipment.IsAvailable = true;
        equipment.BorrowerFirstName = null;
        equipment.BorrowerLastName = null;
        equipment.BorrowDate = null;
        await _context.SaveChangesAsync();
    }

    public async Task<Equipment> FindEquipmentAsync(int id) =>
        await _context.Equipments.FindAsync(id)
        ?? throw new KeyNotFoundException($"Matériel {id} introuvable.");
}
