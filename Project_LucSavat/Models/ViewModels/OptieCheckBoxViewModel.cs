using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models.ViewModels
{
    public class OptieCheckBoxViewModel
    {
        //Properties die overgezet moeten worden naar Optie
        public int Id { get; set; }
        public string Naam { get; set; }
        //Extra propertie om checkboxen te gebruiken
        public bool Checked { get; set; }

    }
}