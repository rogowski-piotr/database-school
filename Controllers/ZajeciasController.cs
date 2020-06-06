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
    public class ZajeciasController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public ZajeciasController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: Zajecias
        public async Task<IActionResult> Index()
        {
            var projekt_SzkolaContext = _context.Zajecia.Include(z => z.Klasa).Include(z => z.Nauczyciel).Include(z => z.Przedmiot);

            return View(await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Zajecias/OrderedByData
        public async Task<IActionResult> OrderedByData()
        {
            var projekt_SzkolaContext = _context.Zajecia.Include(z => z.Klasa).Include(z => z.Nauczyciel).Include(z => z.Przedmiot).OrderBy(z => z.Data);

            return View("~/Views/Zajecias/Index.cshtml", await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Zajecias/OrderedByNauczyciel
        public async Task<IActionResult> OrderedByNauczyciel()
        {
            var projekt_SzkolaContext = _context.Zajecia.Include(z => z.Klasa).Include(z => z.Nauczyciel).Include(z => z.Przedmiot).OrderBy(z => z.NauczycielID);

            return View("~/Views/Zajecias/Index.cshtml", await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Zajecias/OrderedByPrzedmiot
        public async Task<IActionResult> OrderedByPrzedmiot()
        {
            var projekt_SzkolaContext = _context.Zajecia.Include(z => z.Klasa).Include(z => z.Nauczyciel).Include(z => z.Przedmiot).OrderBy(z => z.PrzedmiotID);

            return View("~/Views/Zajecias/Index.cshtml", await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Zajecias/OrderedByKlasa
        public async Task<IActionResult> OrderedByKlasa()
        {
            var projekt_SzkolaContext = _context.Zajecia.Include(z => z.Klasa).Include(z => z.Nauczyciel).Include(z => z.Przedmiot).OrderBy(z => z.KlasaID);

            return View("~/Views/Zajecias/Index.cshtml", await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Zajecias/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia
                .Include(z => z.Klasa)
                .Include(z => z.Nauczyciel)
                .Include(z => z.Przedmiot)
                .FirstOrDefaultAsync(m => m.ZajeciaID == id);
            if (zajecia == null)
            {
                return NotFound();
            }

            return View(zajecia);
        }

        // GET: Zajecias/Create
        public IActionResult Create()
        {
            ViewData["KlasaID"] = new SelectList(_context.Set<Klasa>(), "KlasaID", "KlasaID");
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID");
            ViewData["PrzedmiotID"] = new SelectList(_context.Set<Przedmiot>(), "PrzedmiotID", "PrzedmiotID");
            return View();
        }

        // POST: Zajecias/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ZajeciaID,Data,liczbaGodzinDzien,NauczycielID,KlasaID,PrzedmiotID")] Zajecia zajecia)
        {
            if (ModelState.IsValid)
            {
                _context.Add(zajecia);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Set<Klasa>(), "KlasaID", "KlasaID", zajecia.KlasaID);
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", zajecia.NauczycielID);
            ViewData["PrzedmiotID"] = new SelectList(_context.Set<Przedmiot>(), "PrzedmiotID", "PrzedmiotID", zajecia.PrzedmiotID);
            return View(zajecia);
        }

        // GET: Zajecias/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia.FindAsync(id);
            if (zajecia == null)
            {
                return NotFound();
            }
            ViewData["KlasaID"] = new SelectList(_context.Set<Klasa>(), "KlasaID", "KlasaID", zajecia.KlasaID);
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", zajecia.NauczycielID);
            ViewData["PrzedmiotID"] = new SelectList(_context.Set<Przedmiot>(), "PrzedmiotID", "PrzedmiotID", zajecia.PrzedmiotID);
            return View(zajecia);
        }

        // POST: Zajecias/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ZajeciaID,Data,liczbaGodzinDzien,NauczycielID,KlasaID,PrzedmiotID")] Zajecia zajecia)
        {
            if (id != zajecia.ZajeciaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(zajecia);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ZajeciaExists(zajecia.ZajeciaID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["KlasaID"] = new SelectList(_context.Set<Klasa>(), "KlasaID", "KlasaID", zajecia.KlasaID);
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", zajecia.NauczycielID);
            ViewData["PrzedmiotID"] = new SelectList(_context.Set<Przedmiot>(), "PrzedmiotID", "PrzedmiotID", zajecia.PrzedmiotID);
            return View(zajecia);
        }

        // GET: Zajecias/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = await _context.Zajecia
                .Include(z => z.Klasa)
                .Include(z => z.Nauczyciel)
                .Include(z => z.Przedmiot)
                .FirstOrDefaultAsync(m => m.ZajeciaID == id);
            if (zajecia == null)
            {
                return NotFound();
            }

            return View(zajecia);
        }

        // POST: Zajecias/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var zajecia = await _context.Zajecia.FindAsync(id);
            _context.Zajecia.Remove(zajecia);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ZajeciaExists(int id)
        {
            return _context.Zajecia.Any(e => e.ZajeciaID == id);
        }
    }
}
