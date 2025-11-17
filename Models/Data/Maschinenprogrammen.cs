using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace MaschinenDataein.Models.Data
{
    public class Maschinenprogrammen
    {
        public long Id { get; set; }


        [Required]
        public long MaschinenId { get; set; }
        [ForeignKey(nameof(MaschinenId))]
        public virtual Maschine? Maschine { get; set; }

        public int PRnummer { get; set; }

        public String? Name { get; set; }
    }
}
