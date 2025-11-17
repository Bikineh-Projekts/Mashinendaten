using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MaschinenDataein.Models.Data

{

    public class ZustandsDaten
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
        public long ZustandsmeldungId { get; set; }

        [ForeignKey(nameof(ZustandsmeldungId))]
        public virtual ZustandsMeldung Zustandsmeldung { get; set; } = null!;
      
    }

}