using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models.ViewModels
{
    public class AdminViewModel
    {
        //Properties
        public List<Zoekertje> Zoekertjes { get; set; }
        public List<Vehicle> Vehicles { get; set; }
        public List<Optie> Opties { get; set; }


    }
}