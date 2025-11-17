using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MaschinenDataein.Models.Data
{
    public class TemperaturDaten
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

        public int Solltemp1 { get; set; }

        [Required]

        public int Isstemp1 { get; set; }

        [Required]

        public int Solltemp2 { get; set; }

        [Required]
        public int Isstemp2 { get; set; }

  


    }
}




