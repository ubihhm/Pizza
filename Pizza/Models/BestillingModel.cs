using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models
{
    public class BestillingModel
    {
        public string Brugernavn { get; set; }
        public int AntalMargherita { get; set; }
        public int AntalCapricciosa { get; set; }
        public int AntalQuattroStagioni { get; set; }
    }
}
