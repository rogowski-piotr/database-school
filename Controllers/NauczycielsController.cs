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
    public class NauczycielsController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public NauczycielsController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: Nauczyciels
        public async Task<IActionResult> Index(string searchString)
        {
            var nauczyciele = from n in _context.Nauczyciel 
                              select n;

            if (!String.IsNullOrEmpty(searchString))
            {
                nauczyciele = nauczyciele.Where(s => s.Nazwisko.Contains(searchString));
            }

            return View(await nauczyciele.ToListAsync());
        }

        // GET: Nauczyciels/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel
                .FirstOrDefaultAsync(m => m.NauczycielID == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // GET: Nauczyciels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Nauczyciels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NauczycielID,Imie,Nazwisko,SumaGodzinTygodniowo")] Nauczyciel nauczyciel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nauczyciel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nauczyciel);
        }

        // GET: Nauczyciels/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel.FindAsync(id);
            if (nauczyciel == null)
            {
                return NotFound();
            }
            return View(nauczyciel);
        }

        // POST: Nauczyciels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("NauczycielID,Imie,Nazwisko,SumaGodzinTygodniowo")] Nauczyciel nauczyciel)
        {
            if (id != nauczyciel.NauczycielID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nauczyciel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NauczycielExists(nauczyciel.NauczycielID))
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
            return View(nauczyciel);
        }

        // GET: Nauczyciels/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nauczyciel = await _context.Nauczyciel
                .FirstOrDefaultAsync(m => m.NauczycielID == id);
            if (nauczyciel == null)
            {
                return NotFound();
            }

            return View(nauczyciel);
        }

        // POST: Nauczyciels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var nauczyciel = await _context.Nauczyciel.FindAsync(id);
            _context.Nauczyciel.Remove(nauczyciel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NauczycielExists(string id)
        {
            return _context.Nauczyciel.Any(e => e.NauczycielID == id);
        }
    }
}
