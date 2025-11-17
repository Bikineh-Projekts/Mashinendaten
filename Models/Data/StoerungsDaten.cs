using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaschinenDataein.Models.Data
{
    public class StoerungsDaten
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public DateTime Timestamp { get; set; }

        [Required]
        public long MaschinenId { get; set; }
        [ForeignKey(nameof(MaschinenId))]
        public virtual Maschine? Maschine { get; set; }

        [Required]
        public long StoerungsmeldungId { get; set; }

        [ForeignKey(nameof(StoerungsmeldungId))]
        public virtual StoerungsMeldung Stoerungsmeldung { get; set; } = null!;
    }
}
