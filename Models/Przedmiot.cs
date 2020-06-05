using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Szkola.Models
{
    public class Przedmiot
    {
        [Key]
        public string PrzedmiotID { get; set; }
        public string Nazwa { get; set; }
        public string DlaKlasy { get; set; }
        public int LiczbaGodzinTygodniowo { get; set; }



        // jeden do wielu
        public List<Zajecia> Zajecias { get; set; }

    }
}
