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
    public class UprawnieniesController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public UprawnieniesController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: Uprawnienies
        public async Task<IActionResult> Index(string searchString)
        {
            var uprawnienia = from u in _context.Uprawnienie.Include(u => u.Nauczyciel)
                    select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                uprawnienia = uprawnienia.Where(s => s.UprawnienieNazwa.Contains(searchString));
            }


            return View(await uprawnienia.ToListAsync());
        }

        // GET: Uprawnienies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uprawnienie = await _context.Uprawnienie
                .Include(u => u.Nauczyciel)
                .FirstOrDefaultAsync(m => m.UprawnienieID == id);
            if (uprawnienie == null)
            {
                return NotFound();
            }

            return View(uprawnienie);
        }

        // GET: Uprawnienies/Create
        public IActionResult Create()
        {
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID");
            return View();
        }

        // POST: Uprawnienies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UprawnienieID,UprawnienieNazwa,NauczycielID")] Uprawnienie uprawnienie)
        {
            if (ModelState.IsValid)
            {
                _context.Add(uprawnienie);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", uprawnienie.NauczycielID);
            return View(uprawnienie);
        }

        // GET: Uprawnienies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uprawnienie = await _context.Uprawnienie.FindAsync(id);
            if (uprawnienie == null)
            {
                return NotFound();
            }
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", uprawnienie.NauczycielID);
            return View(uprawnienie);
        }

        // POST: Uprawnienies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UprawnienieID,UprawnienieNazwa,NauczycielID")] Uprawnienie uprawnienie)
        {
            if (id != uprawnienie.UprawnienieID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(uprawnienie);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UprawnienieExists(uprawnienie.UprawnienieID))
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
            ViewData["NauczycielID"] = new SelectList(_context.Nauczyciel, "NauczycielID", "NauczycielID", uprawnienie.NauczycielID);
            return View(uprawnienie);
        }

        // GET: Uprawnienies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var uprawnienie = await _context.Uprawnienie
                .Include(u => u.Nauczyciel)
                .FirstOrDefaultAsync(m => m.UprawnienieID == id);
            if (uprawnienie == null)
            {
                return NotFound();
            }

            return View(uprawnienie);
        }

        // POST: Uprawnienies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var uprawnienie = await _context.Uprawnienie.FindAsync(id);
            _context.Uprawnienie.Remove(uprawnienie);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UprawnienieExists(int id)
        {
            return _context.Uprawnienie.Any(e => e.UprawnienieID == id);
        }
    }
}
