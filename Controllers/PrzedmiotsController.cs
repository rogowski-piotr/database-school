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
    public class PrzedmiotsController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public PrzedmiotsController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: Przedmiots
        public async Task<IActionResult> Index(string searchString)
        {
            var przedmioty = from p in _context.Przedmiot
                             select p;

            if (!String.IsNullOrEmpty(searchString))
            {
                przedmioty = przedmioty.Where(s => s.Nazwa.Contains(searchString));
            }

            return View(await przedmioty.ToListAsync());
            //return View(await _context.Przedmiot.ToListAsync());
        }

        // GET: Przedmiots/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot
                .FirstOrDefaultAsync(m => m.PrzedmiotID == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // GET: Przedmiots/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Przedmiots/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PrzedmiotID,Nazwa,DlaKlasy,LiczbaGodzinTygodniowo")] Przedmiot przedmiot)
        {
            if (ModelState.IsValid)
            {
                _context.Add(przedmiot);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(przedmiot);
        }

        // GET: Przedmiots/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot.FindAsync(id);
            if (przedmiot == null)
            {
                return NotFound();
            }
            return View(przedmiot);
        }

        // POST: Przedmiots/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("PrzedmiotID,Nazwa,DlaKlasy,LiczbaGodzinTygodniowo")] Przedmiot przedmiot)
        {
            if (id != przedmiot.PrzedmiotID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(przedmiot);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PrzedmiotExists(przedmiot.PrzedmiotID))
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
            return View(przedmiot);
        }

        // GET: Przedmiots/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var przedmiot = await _context.Przedmiot
                .FirstOrDefaultAsync(m => m.PrzedmiotID == id);
            if (przedmiot == null)
            {
                return NotFound();
            }

            return View(przedmiot);
        }

        // POST: Przedmiots/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var przedmiot = await _context.Przedmiot.FindAsync(id);
            _context.Przedmiot.Remove(przedmiot);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PrzedmiotExists(string id)
        {
            return _context.Przedmiot.Any(e => e.PrzedmiotID == id);
        }
    }
}
