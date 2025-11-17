using System.ComponentModel.DataAnnotations;

namespace MaschinenDataein.Models.Data;

public class StoerungsMeldung  
{
    [Key]
    public long Id { get; set; }

    [Required]
    [MaxLength(500)]
    public string? Meldung { get; set; }
}
