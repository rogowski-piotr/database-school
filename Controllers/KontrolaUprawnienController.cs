using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Projekt_Szkola.Data;
using Projekt_Szkola.Models;


namespace Projekt_Szkola.Controllers
{
    public class KontrolaUprawnienController : Controller
    {
        private readonly Projekt_SzkolaContext _context;

        public KontrolaUprawnienController(Projekt_SzkolaContext context)
        {
            _context = context;
        }

        // GET: /KontrolaUprawnien
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


        // GET: /KontrolaUprawnien/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var zajecia = _context.Zajecia.Select(n => n).Include(u => u.Nauczyciel).Where(n => n.NauczycielID == id); // zajęcia które prowadził dany nauczyciel

            if (zajecia == null)
            {
                return NotFound();
            }



            var joinZajeciaPrzedmioty = from z in zajecia
                                        join p in _context.Przedmiot
                                        on z.PrzedmiotID equals p.PrzedmiotID
                                        select new
                                        {
                                            z.ZajeciaID,
                                            z.NauczycielID,
                                            z.KlasaID,
                                            z.Data,
                                            z.liczbaGodzinDzien,
                                            p.PrzedmiotID,
                                            p.Nazwa,
                                            z.Nauczyciel.Imie,
                                            z.Nauczyciel.Nazwisko
                                        };

            bool zawiera = true;

            foreach (var z in joinZajeciaPrzedmioty)
            {
                var idNauczyciela = z.NauczycielID;
                var nazwaPrzedmiotu = z.Nazwa;
                bool tmp = false;
                ViewData["Imie"] = z.Imie;
                ViewData["Nazwisko"] = z.Nazwisko;

                foreach (var u in _context.Uprawnienie)
                {
                    if (u.NauczycielID == idNauczyciela && u.UprawnienieNazwa == nazwaPrzedmiotu)
                    {
                        tmp = true;
                    }
                }

                if (!tmp)
                {
                    zawiera = false;
                    ViewData["Data"] = z.Data.ToShortDateString();
                    ViewData["NazwaPrzedmiotu"] = z.Nazwa;
                }
            }

            if (zawiera)
            {
                ViewData["Status"] = "true";
            }
            else
            {
                ViewData["Status"] = "false";
            }
            



                return View(await zajecia.ToListAsync());
        }

    }
}
