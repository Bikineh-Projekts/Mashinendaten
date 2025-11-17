using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MaschinenDataein.Models.Data
{
    [Table("Planungs")] 
    public class Planungs
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime Datum { get; set; }

        [Required]
        public long MaschineID { get; set; }

        // Header - Optional
        public int? Personalsoll { get; set; }

        [StringLength(500)]
        public string? Personalnamen { get; set; }

        // PRODUKTION - alle optional
        [StringLength(100)]
        public string? Artikel { get; set; }

        public int? Sollmenge { get; set; } // Numeric, nicht String!

        [Column(TypeName = "date")]
        public DateTime? MHD { get; set; } // Date, nicht String!

        [StringLength(100)]
        public string? Kartonsanzahl { get; set; }

        public int? PersonalIst { get; set; }

        public int? Fertigware { get; set; } // Numeric, nicht String!

        // ZEITEN - alle optional
        [Column(TypeName = "time")]
        public TimeSpan? Starten { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? Stoppen { get; set; }

        public int? Pause { get; set; } // Minuten als Integer, nicht TimeSpan

        // STÖRUNG - alle optional
        [Column(TypeName = "time")]
        public TimeSpan? Von { get; set; }

        [Column(TypeName = "time")]
        public TimeSpan? Bis { get; set; }

        [StringLength(500)]
        public string? Grund { get; set; }
    }
}
 
