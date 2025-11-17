using MaschinenDataein.Models.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace MaschinenDataein.Models.ModelView
{
    public class DashboardModelView
    {
        public DashboardModelView()
        {
            LeistungsDatenList = new List<LeistungsdatenModelView>();
            ProgrammenList = new List<Maschinenprogrammen>();
            AbzugsDatenList = new List<AbzugsdatenModelView>();
            TemperaturDatenList = new List<TemperaturdatenModelView>();
            AlarmDatenList = new List<AlarmDaten>();
            ZustandsDatenList = new List<ZustandsDaten>();
            ZustandsMeldungList = new List<ZustandsMeldung>();
            StoerungsDatenList = new List<StoerungsDaten>();
            StoerungsMeldungList = new List<StoerungsMeldung>();
        }

        public List<LeistungsdatenModelView> LeistungsDatenList { get; set; }
        public List<Maschinenprogrammen> ProgrammenList { get; set; }
        public List<AbzugsdatenModelView> AbzugsDatenList { get; set; }
        public List<TemperaturdatenModelView> TemperaturDatenList { get; set; }
        public List<AlarmDaten> AlarmDatenList { get; set; }
        public List<ZustandsDaten> ZustandsDatenList { get; set; }
        public List<ZustandsMeldung> ZustandsMeldungList { get; set; }
        public List<StoerungsDaten> StoerungsDatenList { get; set; }
        public List<StoerungsMeldung> StoerungsMeldungList { get; set; }

        public static DashboardModelView GetData(MaschinenDbContext context, List<Maschine> maschinen)
        {
            var modelView = new DashboardModelView();

            // Lade alle Programme vorab – für besseren Zugriff in TemperaturDaten
            var alleProgramme = context.MaschinenProgrammen.ToList();

            foreach (var maschine in maschinen)
            {
                var leistung = context.Leistungsdaten
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (leistung != null)
                    modelView.LeistungsDatenList.Add(new LeistungsdatenModelView(context, leistung));

                var prg = alleProgramme.FirstOrDefault(x => x.MaschinenId == maschine.Id);
                if (prg != null)
                    modelView.ProgrammenList.Add(prg);

                var abzug = context.Abzugsdaten
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (abzug != null)
                    modelView.AbzugsDatenList.Add(new AbzugsdatenModelView(context, abzug));

                var temperatur = context.Temperaturdaten
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (temperatur != null)
                    modelView.TemperaturDatenList.Add(new TemperaturdatenModelView(alleProgramme, temperatur));

                var alarm = context.Alarmdaten
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (alarm != null)
                    modelView.AlarmDatenList.Add(alarm);

                var zustand = context.Zustandsdaten
                    .Include(x => x.Zustandsmeldung)
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (zustand != null)
                    modelView.ZustandsDatenList.Add(zustand);

                var stoerung = context.Stoerungsdaten
                    .Include(x => x.Stoerungsmeldung)
                    .Where(x => x.MaschinenId == maschine.Id)
                    .OrderByDescending(x => x.Timestamp)
                    .FirstOrDefault();

                if (stoerung != null)
                    modelView.StoerungsDatenList.Add(stoerung);
            }

            return modelView;
        }
    }
}
