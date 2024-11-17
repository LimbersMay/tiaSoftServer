using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.Models;

public class UpdateBillDto
{ 
    [Required]
    public string Name { get; set; }
}