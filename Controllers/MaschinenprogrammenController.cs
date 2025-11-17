using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MaschinenDataein.Controllers.Model;
using MaschinenDataein.Helper;
using MaschinenDataein.Models;
using MaschinenDataein.Models.ModelView;
using System.Collections.Generic;
using MaschinenDataein.Models.Data;

namespace MaschinenDataein.Controllers
{
    public class MaschinenprogrammenController : ControllerModel
    {
        public MaschinenprogrammenController(MaschinenDbContext context)
            : base(context, "Maschinenprogrammen")
        {
        }

        public IActionResult Index()
        {
            _actionName = "Data";
            FilterModelView model = GetFilterModelViewCookie();
            SetViewBagFilter(model, "Data", "Maschinenprogrammen");

 
            return View();
        }
        public IActionResult Data()
        {
            _actionName = "Data";
            FilterModelView model = GetFilterModelViewCookie();
            SetViewBagFilter(model, "Index", "Maschinenprogrammen");

            var datumbis = model.DatumBis.Date.AddDays(1).AddSeconds(-1);

            var list = _context.MaschinenProgrammen.ToList();

            if (model.MaschinenId > 0)
            {
                list = list
                    .Where(x => x.MaschinenId == model.MaschinenId)
                    .ToList();
            }

           
            List<Maschinenprogrammen> gefilterteListe = new();
            string? vorherigerName = null;

            foreach (var item in list.OrderBy(x => x.Name))
            {
                var aktuellerName = item.Name;

                if (aktuellerName != vorherigerName)
                {
                    gefilterteListe.Add(item);
                    vorherigerName = aktuellerName;
                }
            }

            return View("Index", gefilterteListe);
        }
    }
}
