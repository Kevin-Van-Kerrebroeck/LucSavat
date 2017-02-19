using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public enum TransmissieType
    {
        Automatisch,
        Manueel,
        [Display(Name="Half Automatisch")]
        HalfAutomatisch,
        [Display(Name = "Alle Transmissies")]
        AlleTransmissies

    }
}