using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public enum Brandstof
    {
        Benzine,
        Diesel,
        LPG,
        Electrisch,
        Hybride,
        [Display(Name = "Alle Transmissies")]
        AlleBrandstoffen,
        [Display(Name = "Overige Transmissies")]
        OverigeTransmissies
    }
}