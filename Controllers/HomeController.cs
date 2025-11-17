using System.Diagnostics;
using MaschinenDataein.Models;
using MaschinenDataein.Models.ModelView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging; // Stellen Sie sicher, dass diese Using-Direktive für ILogger vorhanden ist

namespace MaschinenDataein.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MaschinenDbContext _context; // Fügen Sie das Context-Feld hinzu, wenn es in Ihrer Basisklasse nicht bereits vorhanden ist

        public HomeController(MaschinenDbContext context, ILogger<HomeController> logger) : base()
        {
            _context = context; // Stellen Sie sicher, dass der DbContext ordnungsgemäß initialisiert wird
            _logger = logger;
        }

        public IActionResult Index()
        {
            List<Models.Data.Maschine> maschinen = _context.Maschinen.ToList(); // Stellen Sie sicher, dass die Maschinen erfolgreich abgerufen werden
            if (maschinen == null || maschinen.Count == 0)
            {
                _logger.LogInformation("Keine Maschinen gefunden."); // Loggen Sie den Fall, dass keine Maschinen gefunden werden
                return View("Error"); // Leiten Sie zu einer Fehlerseite um, oder wie in Ihrem Fall angezeigt, geben Sie null zurück
            }

            var modelView = DashboardModelView.GetData(_context, maschinen); // Holen Sie die Daten über die DashboardModelView Klasse
            return View(modelView); // Geben Sie das ModelView an die View zurück
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
