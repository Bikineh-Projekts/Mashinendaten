using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using MaschinenDataein.Helper;
using MaschinenDataein.Models.Data;
using MaschinenDataein.Models.ModelView;
using MaschinenDataein.Models;

namespace MaschinenDataein.Controllers
{
    public class PlanungController : Controller
    {
        private readonly MaschinenDbContext _context;

        public PlanungController(MaschinenDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var modelView = new PlanungModelView();
            ViewBag.Maschinen = DropDownListHelper.GetMaschinen(_context);
            return View(modelView);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Save(PlanungModelView modelView)
        {
            // Keine ModelState/Datum/Maschine-Bedingungen mehr

            var g = modelView?.GrunddatenModelView ?? new GrunddatenModelView();

            // Fallbacks: wenn Nutzer nichts gewählt hat
            var fallbackDatum = g.Datum ?? DateTime.Today;

            // wenn es Maschinen gibt, nimm die erste; sonst 0 (kann bei FK-Constraint fehlschlagen)
            var firstMaschineId = _context.Maschinen.Select(m => m.Id).FirstOrDefault();
            var fallbackMaschineId = g.MaschineID > 0 ? g.MaschineID : firstMaschineId;

            // Zeilen filtern
            var rows = modelView?.ProduktionserfassungModelViews ?? new List<ProduktionserfassungModelView>();
            var valideZeilen = rows.Where(z => !IstLeereZeile(z)).ToList();

            if (!valideZeilen.Any())
            {
                // Header-only speichern (Zeilenfelder bleiben NULL)
                var headerOnly = new Planungs
                {
                    Datum = fallbackDatum,
                    MaschineID = fallbackMaschineId,
                    Personalsoll = g.Personalsoll,
                    Personalnamen = g.Personalnamen
                };

                _context.Planung.Add(headerOnly);
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Grunddaten gespeichert.";
                return RedirectToAction(nameof(Index));
            }

            // Mit Zeilen speichern
            var entities = valideZeilen.Select(z => new Planungs
            {
                // Header (mit Fallbacks)
                Datum = fallbackDatum,
                MaschineID = fallbackMaschineId,
                Personalsoll = g.Personalsoll,
                Personalnamen = g.Personalnamen,

                // Zeilen
                Artikel = z.Artikel,
                Sollmenge = z.Sollmenge,
                MHD = z.MHD,
                Kartonsanzahl = z.Kartonsanzahl,
                PersonalIst = z.PersonalIst,
                Fertigware = z.Fertigware,
                Starten = z.Starten,
                Stoppen = z.Stoppen,
                Pause = z.Pause
            }).ToList();

            _context.Planung.AddRange(entities);
            _context.SaveChanges();

            TempData["SuccessMessage"] = $"Erfolgreich {entities.Count} Zeile(n) gespeichert.";
            return RedirectToAction(nameof(Index));
        }

        private static bool IstLeereZeile(ProduktionserfassungModelView z)
        {
            if (z == null) return true;
            var artikelLeer = string.IsNullOrWhiteSpace(z.Artikel?.Trim());
            var kartonsLeer = string.IsNullOrWhiteSpace(z.Kartonsanzahl?.Trim());

            return artikelLeer
                && z.Sollmenge == null
                && z.MHD == null
                && kartonsLeer
                && z.PersonalIst == null
                && z.Fertigware == null
                && z.Starten == null
                && z.Stoppen == null
                && z.Pause == null;
        }
    }
}




// Zeilen holen und leere Zeilen entfernen var rows = modelView.ProduktionserfassungModelViews ?? new List<ProduktionserfassungModelView>(); var valideZeilen = rows.Where(z => !IstLeereZeile(z)).ToList(); if (valideZeilen.Count == 0) { ModelState.AddModelError(string.Empty, "Bitte mindestens eine Produktionszeile ausfüllen."); ViewBag.Maschinen = DropDownListHelper.GetMaschinen(_context); return View("Index", modelView); } // Header (Grunddaten) -> in jede Entity kopieren var g = modelView.GrunddatenModelView; var entities = valideZeilen.Select(z => new Planungs { // Header-Felder Datum = (DateTime)g.Datum, MaschineID = g.MaschineID, Personalsoll = g.Personalsoll, Personalnamen = g.Personalnamen, // Zeilen-Felder Artikel = z.Artikel, Sollmenge = z.Sollmenge, MHD = z.MHD, Kartonsanzahl = z.Kartonsanzahl, PersonalIst = z.PersonalIst, Fertigware = z.Fertigware, Starten = z.Starten, Stoppen = z.Stoppen, Pause = z.Pause, }).ToList(); // In die EINZIGE Tabelle "Planung" speichern _context.Planung.AddRange(entities); _context.SaveChanges(); TempData["SuccessMessage"] = $"Erfolgreich {entities.Count} Zeile(n) gespeichert."; return RedirectToAction(nameof(Index)); } // Hilfsfunktion: entscheidet, ob eine Zeile "leer" ist private static bool IstLeereZeile(ProduktionserfassungModelView z) => string.IsNullOrWhiteSpace(z.Artikel) && z.Sollmenge == null && z.MHD == null && string.IsNullOrWhiteSpace(z.Kartonsanzahl) && z.PersonalIst == null && z.Fertigware == null && z.Starten == null && z.Stoppen == null && z.Pause == null; } }




/*[HttpPost]
[ValidateAntiForgeryToken]*/
/*public async Task<IActionResult> Speichern( 
    [Bind(Prefix = "Item1")] GrunddatenModelView item1,
    string? RowsJson,
    CancellationToken ct)
{
    try
    {
        // 1) Grunddaten validieren
        if (!ValidateGrunddaten(item1, out var grunddatenErrors))
        {
            foreach (var error in grunddatenErrors)
                ModelState.AddModelError(string.Empty, error);
        }

        // 2) RowsJson -> Liste
        var produktionszeilen = new List<ProduktionserfassungModelView>();
        if (!string.IsNullOrWhiteSpace(RowsJson))
        {
            try
            {
                // TimeSpan "HH:mm:ss" wird von STJ in .NET 7+ unterstützt
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                produktionszeilen = JsonSerializer.Deserialize<List<ProduktionserfassungModelView>>(RowsJson, options)
                                   ?? new List<ProduktionserfassungModelView>();
            }
            catch
            {
                ModelState.AddModelError(string.Empty, "Fehler beim Verarbeiten der Produktionsdaten.");
            }
        }

        // 3) Leere Zeilen raus + pro Zeile validieren
        var gueltig = new List<ProduktionserfassungModelView>();
        for (int i = 0; i < produktionszeilen.Count; i++)
        {
            var row = produktionszeilen[i];
            if (row == null || row.IstLeereZeile()) continue;

            var errs = ValidateProduktionserfassung(row, i + 1);
            foreach (var e in errs)
                ModelState.AddModelError($"Produktionszeile[{i}]", e);

            gueltig.Add(row);
        }

        if (gueltig.Count == 0)
            ModelState.AddModelError(string.Empty, "Mindestens eine Produktionszeile muss ausgefüllt werden.");

        if (!ModelState.IsValid)
        {
            var backModel = new Tuple<GrunddatenModelView, List<ProduktionserfassungModelView>>(
                item1,
                produktionszeilen);

            TempData["Error"] = "Bitte korrigieren Sie die Eingabefehler.";
            return View("Index", backModel);
        }

        // 4) Speichern (Transaktion)
        using var tx = await _context.Database.BeginTransactionAsync(ct);
        try
        {
            var entities = gueltig.Select(p => new Planungs
            {
                // Header
                Datum = item1.Datum!.Value,
                MaschineID = item1.MaschinenID!.Value,
                Personalsoll = item1.Personalsoll,
                Personalnamen = item1.Personalnamen,

                // Zeile
                Artikel = TrimOrNull(p.Artikel),
                Sollmenge = p.Sollmenge,
                Starten = p.Starten,
                Stoppen = p.Stoppen,
                Pause = p.Pause,
                MHD = p.MHD,
                Kartonsanzahl = TrimOrNull(p.Kartonsanzahl),
                PersonalIst = p.PersonalIst,
                Fertigware = p.Fertigware
            }).ToList();

            await _context.Planung.AddRangeAsync(entities, ct);
            await _context.SaveChangesAsync(ct);
            await tx.CommitAsync(ct);
        }
        catch
        {
            await tx.RollbackAsync(ct);
            throw;
        }

        TempData["Success"] = $"Produktionsplanung erfolgreich gespeichert. {gueltig.Count} Produktionszeile(n) verarbeitet.";
        return RedirectToAction(nameof(Index));
    }
    catch (DbUpdateException ex)
    {
        TempData["Error"] = "Fehler beim Speichern in die Datenbank: " + ex.GetBaseException().Message;
        var backModel = new Tuple<GrunddatenModelView, List<ProduktionserfassungModelView>>(item1, new());
        return View("Index", backModel);
    }
    catch (Exception ex)
    {
        TempData["Error"] = "Ein unerwarteter Fehler ist aufgetreten: " + ex.Message;
        var backModel = new Tuple<GrunddatenModelView, List<ProduktionserfassungModelView>>(item1, new());
        return View("Index", backModel);
    }
}*/

/*private static string? TrimOrNull(string? s)
    => string.IsNullOrWhiteSpace(s) ? null : s.Trim();

private bool ValidateGrunddaten(GrunddatenModelView grunddaten, out List<string> errors)
{
    errors = new();
    if (grunddaten == null)
    {
        errors.Add("Grunddaten sind erforderlich.");
        return false;
    }

    if (!grunddaten.Datum.HasValue)
        errors.Add("Datum ist erforderlich.");
    else if (grunddaten.Datum.Value > DateTime.Today.AddDays(30))
        errors.Add("Datum darf nicht mehr als 30 Tage in der Zukunft liegen.");

    if (!grunddaten.MaschinenID.HasValue)
        errors.Add("Maschine muss ausgewählt werden.");
    else if (!IstGueltigeMaschinenID(grunddaten.MaschinenID.Value))
        errors.Add("Ungültige Maschinen-ID ausgewählt.");

    if (grunddaten.Personalsoll is < 0 or > 20)
        errors.Add("Personal Soll muss zwischen 0 und 20 liegen.");

    if (!string.IsNullOrEmpty(grunddaten.Personalnamen) && grunddaten.Personalnamen.Length > 500)
        errors.Add("Personalnamen dürfen maximal 500 Zeichen lang sein.");

    return errors.Count == 0;
}

private List<string> ValidateProduktionserfassung(ProduktionserfassungModelView item, int zeilenNummer)
{
    var errors = new List<string>();

    if (string.IsNullOrWhiteSpace(item.Artikel) && !item.IstLeereZeile())
        errors.Add($"Zeile {zeilenNummer}: Artikel ist erforderlich.");

    if (!string.IsNullOrEmpty(item.Artikel) && item.Artikel.Length > 100)
        errors.Add($"Zeile {zeilenNummer}: Artikel darf maximal 100 Zeichen lang sein.");

    if (item.Sollmenge.HasValue && item.Sollmenge <= 0)
        errors.Add($"Zeile {zeilenNummer}: Sollmenge muss größer als 0 sein.");

    if (item.Starten.HasValue && item.Stoppen.HasValue && item.Stoppen <= item.Starten)
        errors.Add($"Zeile {zeilenNummer}: Stopp-Zeit muss nach Start-Zeit liegen.");

    if (item.Pause.HasValue && item.Pause < 0)
        errors.Add($"Zeile {zeilenNummer}: Pause kann nicht negativ sein.");

    if (item.MHD.HasValue && item.MHD < DateTime.Today)
        errors.Add($"Zeile {zeilenNummer}: MHD darf nicht in der Vergangenheit liegen.");

    if (!string.IsNullOrEmpty(item.Kartonsanzahl) && item.Kartonsanzahl.Length > 100)
        errors.Add($"Zeile {zeilenNummer}: Kartonsanzahl darf maximal 100 Zeichen lang sein.");

    if (item.PersonalIst.HasValue && item.PersonalIst < 0)
        errors.Add($"Zeile {zeilenNummer}: Personal Ist kann nicht negativ sein.");

    if (item.Fertigware.HasValue && item.Fertigware < 0)
        errors.Add($"Zeile {zeilenNummer}: Fertigware kann nicht negativ sein.");

    if (item.Fertigware.HasValue && item.Sollmenge.HasValue && item.Fertigware > item.Sollmenge)
        errors.Add($"Zeile {zeilenNummer}: Fertigware ({item.Fertigware}) sollte nicht größer als Sollmenge ({item.Sollmenge}) sein.");

    return errors;
}

private bool IstGueltigeMaschinenID(long maschinenID)
{
    var gueltigeMaschinen = new long[] { 1, 2, 4 };
    return gueltigeMaschinen.Contains(maschinenID);
}*/
