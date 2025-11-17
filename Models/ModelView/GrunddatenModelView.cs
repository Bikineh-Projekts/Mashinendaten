using System;
using System.ComponentModel.DataAnnotations;

namespace MaschinenDataein.Models.ModelView
{
    
    public class GrunddatenModelView
    {
        internal long MaschineID;

        public GrunddatenModelView()
        {
            Datum = DateTime.Now;
        }
        [Required(ErrorMessage = "Datum ist erforderlich")]
        [DataType(DataType.Date)]
        public DateTime? Datum { get; set; }

        [Required(ErrorMessage = "Maschine muss ausgewählt werden")]
        public long? MaschinenID { get; set; }

        [Range(0, 20, ErrorMessage = "Personal Soll muss zwischen 0 und 20 liegen")]
        public int? Personalsoll { get; set; }

        [StringLength(500, ErrorMessage = "Maximal 500 Zeichen erlaubt")]
        public string? Personalnamen { get; set; }
    }
}
