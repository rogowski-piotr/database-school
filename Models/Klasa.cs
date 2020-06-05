using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace Projekt_Szkola.Models
{
    public class Klasa
    {
        [Display(Name = "Identyfikator")]
        [Key]
        public string KlasaID { get; set; }
        public string Sala { get; set; }




        // jeden do wielu
        public List<Zajecia> Zajecias { get; set; }
        // jeden do jednego
        [Display(Name = "Identyfikator Wychowawcy")]
        public string NauczycielID { get; set; }
        [Display(Name = "Wychowawca")]
        public Nauczyciel Nauczyciel { get; set; }

    }
}
