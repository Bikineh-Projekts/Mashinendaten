using MaschinenDataein.Models.ModelView;
using System.ComponentModel.DataAnnotations;



namespace MaschinenDataein.Models.ModelView
{

    public class ProduktionserfassungModelView

    {
        [StringLength(100)]
        public string? Artikel { get; set; }

        public int? Sollmenge { get; set; }

        public TimeSpan? Starten { get; set; }   // <input type="time">
        public TimeSpan? Stoppen { get; set; }   // <input type="time">

        public int? Pause { get; set; }          // Minuten

        public DateTime? MHD { get; set; }       // <input type="date">

        [StringLength(100)]
        public string? Kartonsanzahl { get; set; }

        public int? PersonalIst { get; set; }
        public int? Fertigware { get; set; }


      
    }
}
