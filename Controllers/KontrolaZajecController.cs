using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt_Szkola.Data;
using Projekt_Szkola.Models;


namespace Projekt_Szkola.Controllers
{
    public class KontrolaZajecController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public KontrolaZajecController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: /KontrolaZajec
        public async Task<IActionResult> Index()
        {
            var projekt_SzkolaContext = _context.Klasa.Include(k => k.Nauczyciel);
            return View(await projekt_SzkolaContext.ToListAsync());
        }

        // GET: /KontrolaZajec/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _context.Zajecia.Select(n => n).Where(n => n.KlasaID == id);

            int zrealizowane = result.Sum(n => n.liczbaGodzinDzien);
            ViewData["Zrealizowane"] = zrealizowane;

            ViewData["Klasa"] = id;
            id = id[0].ToString();

            int zadeklarowane = _context.Przedmiot.Select(n => n).Where(n => n.DlaKlasy == id).Sum(n => n.LiczbaGodzinTygodniowo);
            ViewData["Zadeklarowane"] = zadeklarowane;

            if (zadeklarowane - zrealizowane == 0)
                ViewData["Zrealizowany plan"] = "Tak";
            else
                ViewData["Zrealizowany plan"] = "Nie";

            if (result == null)
            {
                return NotFound();
            }

            return View(await _context.Klasa.ToListAsync());
        }


    }
}
