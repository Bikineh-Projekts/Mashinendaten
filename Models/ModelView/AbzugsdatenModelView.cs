using MaschinenDataein.Models.Data;


namespace MaschinenDataein.Models.ModelView
{
    public class AbzugsdatenModelView
    {
        public AbzugsdatenModelView(List<Maschinenprogrammen> programme)
        {

        }
        public AbzugsdatenModelView(MaschinenDbContext context, AbzugsDaten AbzugsDaten)
        {
            Id = AbzugsDaten.Id;
            Timestamp = AbzugsDaten.Timestamp;
#pragma warning disable CS8602 // Dereferenzierung eines möglichen Nullverweises.
            Maschine = AbzugsDaten.Maschine.Bezeichnung;
#pragma warning restore CS8602 // Dereferenzierung eines möglichen Nullverweises.
            Name = HoleProgrammenamen(context, AbzugsDaten.MaschinenId, AbzugsDaten.PRnummer);
            PackungenproAbzug = AbzugsDaten.PackungenproAbzug;
            Abzuglaenge = AbzugsDaten.Abzuglaenge;

        }
    

        public long Id { get; set; }
        public DateTime Timestamp { get; set; }
        public string? Maschine { get; set; }
        public string? Name{ get; set; }
        public long PackungenproAbzug { get; set; }
        public long Abzuglaenge { get; set; }
        private string? HoleProgrammenamen(MaschinenDbContext context, long maschinenId, int prNr)
        {
            var programm = context.MaschinenProgrammen
                .FirstOrDefault(p => p.MaschinenId == maschinenId && p.PRnummer == prNr);

            return programm?.Name;
        }
    }
}
