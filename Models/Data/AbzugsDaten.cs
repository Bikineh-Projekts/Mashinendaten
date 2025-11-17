using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MaschinenDataein.Models.Data;

public class AbzugsDaten
{
    public long Id { get; set; }

    [Required]
    public DateTime Timestamp { get; set; }

    [Required]
    public long MaschinenId { get; set; }
    [ForeignKey(nameof(MaschinenId))]

    public virtual Maschine? Maschine { get; set; }

    [Required]
    public int PRnummer { get; set; }

    [Required]
    public long PackungenproAbzug { get; set; }

    [Required]
    public long Abzuglaenge { get; set; }

}
