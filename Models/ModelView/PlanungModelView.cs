namespace MaschinenDataein.Models.ModelView
{
    public class PlanungModelView
    {
        public PlanungModelView()
        {
            GrunddatenModelView = new();
            ProduktionserfassungModelViews = new List<ProduktionserfassungModelView>() { new() }; 
        }
        public GrunddatenModelView GrunddatenModelView { get; set; }
        public List<ProduktionserfassungModelView> ProduktionserfassungModelViews { get; set; }
    }
}
