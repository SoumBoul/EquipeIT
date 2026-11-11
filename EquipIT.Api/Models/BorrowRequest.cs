using System.ComponentModel.DataAnnotations;

namespace EquipIT.Api.Models;

public class BorrowRequest
{
    [Required(ErrorMessage = "Le prénom est obligatoire.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Le nom est obligatoire.")]
    public string LastName { get; set; } = string.Empty;
}
