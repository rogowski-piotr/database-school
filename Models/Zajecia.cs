using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Szkola.Models
{
    public class Zajecia
    {
        [Key]
        public int ZajeciaID { get; set; }

        [DataType(DataType.Date)]
        public DateTime Data { get; set; }
        [Display(Name = "Ilość godzin (dziennie)")]
        public int liczbaGodzinDzien { get; set; }



        // wiele do jednego
        [Display(Name = "Identyfikator nauczyciela")]
        public string NauczycielID { get; set; }
        public Nauczyciel Nauczyciel { get; set; }


        // wiele do jednego
        [Display(Name = "Identyfikator klasy")]
        public string KlasaID { get; set; }
        public Klasa Klasa { get; set; }


        // wiele do jednego
        [Display(Name = "Identyfikator przedmiotu")]
        public string PrzedmiotID { get; set; }
        public Przedmiot Przedmiot { get; set; }


    }
}
