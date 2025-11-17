using System.ComponentModel.DataAnnotations.Schema;

namespace MaschinenDataein.Models.Data
{

    public class LeistungsDaten
    {
        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public long MaschinenId { get; set; }
        [ForeignKey(nameof(MaschinenId))]
        public virtual Maschine? Maschine { get; set; }
        public int PRnummer { get; set; }
        public int Tagestaktzaehler { get; set; }
        public int Packungszaeler { get; set; }
        public int Maschinentakte { get; set; }
    
    }
}

