using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class BrugerModel
    {
        public string Brugernavn { get; set; }
        public string Navn { get; set; }
        public string Gade { get; set; }
        public int Postnummer { get; set; }
        public string Bynavn { get; set; }
    }
}
