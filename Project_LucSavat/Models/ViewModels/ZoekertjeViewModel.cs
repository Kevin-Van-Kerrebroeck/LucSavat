using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project_LucSavat.Models.ViewModels
{
    public class ZoekertjeViewModel
    {
        public Zoekertje Zoekertje { get; set; }
        public SelectList Vehicles { get; set; }

    }
}