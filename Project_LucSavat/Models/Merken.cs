using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public enum Merken
    {
        [Display(Name = "Alfa Romeo")]
        AlfaRomeo,
        Audi,
        Bentley,
        BMW,
        Chevrolet,
        Citroën,
        Chrystler,
        Dacia,
        Fiat,
        Ford,
        Honda,
        Hyundai,
        Kia,
        Lexus,
        Mazda,
        Mercedes,
        Mitsubishi,
        Nissan,
        Opel,
        Peugot,
        Renault,
        Seat,
        Skoda,
        Toyata,
        Volkswagen,
        Volvo,
        [Display(Name = "Overige merken")]
        Overige,
        [Display(Name = "Alle merken")]
        AlleMerken
    }
}