using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Pizza.Models.View
{
    public class BestillingViewModel
    {
      
        [Required(ErrorMessage = "Brugernavn skal udfyldes")]
        public string Brugernavn { get; set; }
        [Display(Name = "Pizza Margherita")]
        [Range(0, 9, ErrorMessage = "Bestil et rimeligt antal (0-9)")]
        public int AntalMargherita { get; set; }
        [Display(Name = "Pizza Capricciosa")]
        [Range(0, 9, ErrorMessage = "Bestil et rimeligt antal (0-9)")]
        public int AntalCapricciosa { get; set; }
        [Display(Name = "Pizza Quattro Stagioni")]
        [Range(0, 9, ErrorMessage = "Bestil et rimeligt antal (0-9)")]
        public int AntalQuattroStagioni { get; set; }
        public string Meddelelse { get; set; }
    }
}

