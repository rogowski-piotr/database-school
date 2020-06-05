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
    public class KlasasController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public KlasasController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: Klasas
        public async Task<IActionResult> Index()
        {
            var projekt_SzkolaContext = _context.Klasa.Include(k => k.Nauczyciel);
            return View(await projekt_SzkolaContext.ToListAsync());
        }

        // GET: Klasas/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasa
                .Include(k => k.Nauczyciel)
                .FirstOrDefaultAsync(m => m.KlasaID == id);
            if (klasa == null)
            {
                return NotFound();
            }

            return View(klasa);
        }

        // GET: Klasas/Create
        public IActionResult Create()
        {
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID");
            return View();
        }

        // POST: Klasas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("KlasaID,Sala,NauczycielID")] Klasa klasa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(klasa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", klasa.NauczycielID);
            return View(klasa);
        }

        // GET: Klasas/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasa.FindAsync(id);
            if (klasa == null)
            {
                return NotFound();
            }
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", klasa.NauczycielID);
            return View(klasa);
        }

        // POST: Klasas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("KlasaID,Sala,NauczycielID")] Klasa klasa)
        {
            if (id != klasa.KlasaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(klasa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KlasaExists(klasa.KlasaID))
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
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", klasa.NauczycielID);
            return View(klasa);
        }

        // GET: Klasas/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var klasa = await _context.Klasa
                .Include(k => k.Nauczyciel)
                .FirstOrDefaultAsync(m => m.KlasaID == id);
            if (klasa == null)
            {
                return NotFound();
            }

            return View(klasa);
        }

        // POST: Klasas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var klasa = await _context.Klasa.FindAsync(id);
            _context.Klasa.Remove(klasa);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KlasaExists(string id)
        {
            return _context.Klasa.Any(e => e.KlasaID == id);
        }
    }
}
