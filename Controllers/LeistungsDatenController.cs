using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MaschinenDataein.Controllers.Model;
using MaschinenDataein.Helper;
using MaschinenDataein.Models;
using MaschinenDataein.Models.ModelView;
using MaschinenDataein.Models.PaginatedModel;
using X.PagedList;
using X.PagedList.Extensions;

namespace MaschinenDataein.Controllers
{
    public class LeistungsDatenController : ControllerModel
    {

        public LeistungsDatenController(MaschinenDbContext context)
            : base(context, "Leistungsdaten")
        {
        }

        public IActionResult Index()
        {
            // Legt fest, dass die nächste Action "Data" heißen soll:
            _actionName = "Data";

            // KEINE Parameterübergabe an GetFilterModelViewCookie!
            FilterModelView model = GetFilterModelViewCookie();

            // Zusätzliche Daten für die ViewBag, z. B. Maschinenliste:
            SetViewBagFilter(model, "Data", "Leistungsdaten");

            ViewData["IsShowId"] = true;

            return View();
        }

        public IActionResult Data(int pageNumber)
        {
            _actionName = "Data";
            FilterModelView model = GetFilterModelViewCookie();
            SetViewBagFilter(model, "Index", "Leistungsdaten");

            // Beispiel: Datumsfilter
            var datumbis = model.DatumBis.AddHours(23).AddMinutes(59).AddSeconds(59);

            // Filtern nach Timestamp
            var list = _context.Leistungsdaten.Include(x => x.Maschine)
                .Where(x => x.Timestamp >= model.DatumVon && x.Timestamp <= datumbis)
                .ToList();

            // Optionaler Maschinen-Filter
            if (model.MaschinenId > 0)
            {
                list = list
                    .Where(x => x.MaschinenId == model.MaschinenId)
                    .ToList();
            }
            var programme = _context.MaschinenProgrammen.Include(x => x.Maschine)
                .ToList();
            List<LeistungsdatenModelView> modelView = list.Select(x=> new LeistungsdatenModelView(_context, x)).ToList();
            if (pageNumber <= 0)
            {
                pageNumber = 1;
            }
            var pagedList = modelView.ToPagedList(pageNumber, 50);

            modelView = pagedList.ToList();


            PaginatedListItem paginatedListItem = new(pagedList.PageNumber, pagedList.PageCount, pagedList.IsFirstPage, pagedList.IsLastPage, "Data");
            ViewData["PaginatedListItem"] = paginatedListItem;


            // ID-Anzeige aktivieren
            ViewData["IsShowId"] = true;

            // Rückgabe an Index-View mit paginierten Daten
            return View("Index", modelView);
        }
    }
}
