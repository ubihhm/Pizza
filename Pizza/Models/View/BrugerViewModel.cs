using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models.View
{
    public class BrugerViewModel
    {
        [Required]
        public string Brugernavn { get; set; }
        public string Navn { get; set; }
        public string Gade { get; set; }
        [Range(1000, 9999)]
        public int Postnummer { get; set; }
        public string Bynavn { get; set; }
        public string Meddelelse { get; set; }
        public bool visLink { get; set; }
    }
}

