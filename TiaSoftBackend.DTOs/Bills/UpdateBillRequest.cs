using System.ComponentModel.DataAnnotations;

namespace TiaSoftBackend.DTOs.Bills;

public class UpdateBillRequest
{
    [Required]
    public required string Name { get; set; }
}