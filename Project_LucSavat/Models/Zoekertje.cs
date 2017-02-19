using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public class Zoekertje
    {
        //Id
        public int Id { get; set; }
        public string Title { get; set; }
        public decimal Prijs { get; set; }
        public string Beschrijving { get; set; }
        //FK
        public int VehicleId { get; set; }
        //Navigation property
        public Vehicle Vehicle { get; set; }
    }
}