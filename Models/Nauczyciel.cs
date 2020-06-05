using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Szkola.Models
{
    public class Nauczyciel
    {
        [Display(Name = "Identyfikator nauczyciela")]
        [Key]
        public string NauczycielID { get; set; }
        public string Imie { get; set; }
        public string Nazwisko { get; set; }
       
        [Display(Name = "Ilość godzin (tygodniowo)")]
        public int SumaGodzinTygodniowo { get; set; }



        // jeden do wielu
        public List<Zajecia> Zajecias { get; set; }
        // jeden do wielu
        public List<Uprawnienie> Uprawnienies { get; set; }
        // jeden do jednego
        public Klasa Klasa { get; set; }
    }
}
