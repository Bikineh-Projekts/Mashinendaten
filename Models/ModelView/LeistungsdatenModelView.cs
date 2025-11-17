using MaschinenDataein.Models.Data;
using System;
using System.Linq;

namespace MaschinenDataein.Models.ModelView
{
    public class LeistungsdatenModelView
    {
        public LeistungsdatenModelView(List<Maschinenprogrammen> programme) { }

        public LeistungsdatenModelView(MaschinenDbContext context, LeistungsDaten leistungsDaten)
        {
            Id = leistungsDaten.Id;
            Timestamp = leistungsDaten.Timestamp;
            MaschinenId = leistungsDaten.MaschinenId;

            // Maschinenbezeichnung sicher lesen
            Maschine = leistungsDaten.Maschine?.Bezeichnung;

            // Programmnamen sicher zuordnen
            Name = HoleProgrammenamen(context, leistungsDaten.MaschinenId, leistungsDaten.PRnummer);

            Tagestaktzaehler = leistungsDaten.Tagestaktzaehler;
            Packungszaehler = leistungsDaten.Packungszaeler;
            Maschinentakte = leistungsDaten.Maschinentakte;
        }

        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public long MaschinenId { get; set; }
        public string? Maschine { get; set; }
        public string? Name { get; set; }
        public int Tagestaktzaehler { get; set; }
        public int Packungszaehler { get; set; }
        public int Maschinentakte { get; set; }

        private string? HoleProgrammenamen(MaschinenDbContext context, long maschinenId, int prNr)
        {
            var programm = context.MaschinenProgrammen
                .FirstOrDefault(p => p.MaschinenId == maschinenId && p.PRnummer == prNr);

            return programm?.Name;
        }
    }
}
