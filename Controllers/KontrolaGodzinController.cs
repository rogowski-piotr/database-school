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
    public class KontrolaGodzinController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public KontrolaGodzinController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        public async Task<IActionResult> Index()
        {
            return View(await _context.Nauczyciel.ToListAsync());
        }

        // GET: /<controller>/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var result = _context.Zajecia.Select(n => n).Where(n => n.NauczycielID == id);
            
            int przepracowane = result.Sum(n => n.liczbaGodzinDzien);
            ViewData["Przepracowane"] = przepracowane;
            
            int zadeklarowane = _context.Nauczyciel.Select(n => n).Where(n => n.NauczycielID == id).Sum(n => n.SumaGodzinTygodniowo);
            ViewData["Zadeklarowane"] = zadeklarowane;
            
            if (przepracowane > zadeklarowane)
                ViewData["RóżnicaNazwa"] = "Nadgodziny";
            else
                ViewData["RóżnicaNazwa"] = "Do odpracowania";

            ViewData["Różnica"] = Math.Abs(zadeklarowane - przepracowane);

            if (result == null)
            {
                return NotFound();
            }

            return View(result);
        }


    }
}
