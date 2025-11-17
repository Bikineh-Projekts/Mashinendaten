using MaschinenDataein.Models;
using MaschinenDataein.Models.Data;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace MaschinenDataein.Helper
{
    public class DropDownListHelper
    {
        public static SelectList GetMaschinen(MaschinenDbContext context, bool isAll = false)
        {
            var maschinen = context.Maschinen.OrderBy(m => m.Bezeichnung).ToList();

            if (isAll)
            {
                maschinen.Insert(0, new Maschine { Id = 0, Bezeichnung = "Alle" });
            }

            return new SelectList(maschinen, "Id", "Bezeichnung");
        }
    }
}
