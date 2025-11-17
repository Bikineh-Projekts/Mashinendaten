using MaschinenDataein.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaschinenDataein.Models.ModelView
{
    public class TemperaturdatenModelView
    {
        public TemperaturdatenModelView() { }

        public TemperaturdatenModelView(List<Maschinenprogrammen> programme, TemperaturDaten temperaturDaten)
        {
            Id = temperaturDaten.Id;
            Timestamp = temperaturDaten.Timestamp;
            Maschine = temperaturDaten.Maschine?.Bezeichnung;
            MaschinenId = temperaturDaten.MaschinenId;
            Name = HoleProgrammnamen(programme, temperaturDaten.PRnummer);
            Solltemp1 = temperaturDaten.Solltemp1;
            Isstemp1 = temperaturDaten.Isstemp1;
            Solltemp2 = temperaturDaten.Solltemp2;
            Isstemp2 = temperaturDaten.Isstemp2;
        }

        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Maschine { get; set; }
        public long MaschinenId { get; set; }
        public string? Name { get; set; }
        public int Solltemp1 { get; set; }
        public int Isstemp1 { get; set; }
        public int Solltemp2 { get; set; }
        public int Isstemp2 { get; set; }

        private string? HoleProgrammnamen(List<Maschinenprogrammen> programme, int programmnummer)
        {
            return programme.FirstOrDefault(p => p.PRnummer == programmnummer)?.Name;
        }
    }
}
