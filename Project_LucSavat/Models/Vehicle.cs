using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public class Vehicle
    {
        //PROPERTIES
        //Key
        public int Id { get; set; }

        //Identificatie van voertuig
        public Merken Merk { get; set; }
        public string Type { get; set; }

        //Specificaties
        public DateTime EersteInschrijving { get; set; }
        public int KmStand { get; set; }
        public TransmissieType Transmissie { get; set; }
        public Brandstof Brandstof { get; set; }
        public string Kleur { get; set; }
        public int AantalDeuren { get; set; }

        public List<Optie> Opties { get; set; }

        //CONSTRUCTORS
        public Vehicle()
        {
            Opties = new List<Optie>();
        }


    }
}