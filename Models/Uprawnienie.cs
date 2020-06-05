using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Projekt_Szkola.Models
{
    public class Uprawnienie
    {
        [Key]
        public int UprawnienieID { get; set; }

        public string UprawnienieNazwa { get; set; }



        // wiele do jednego
        public string NauczycielID { get; set; }
        public Nauczyciel Nauczyciel { get; set; }
    }
}
