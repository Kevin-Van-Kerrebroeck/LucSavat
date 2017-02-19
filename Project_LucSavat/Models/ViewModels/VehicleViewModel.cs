using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_LucSavat.Models.ViewModels
{
    public class VehicleViewModel
    {

        public Vehicle Vehicle { get; set; }
        public SelectList Opties { get; set; }
    }
}