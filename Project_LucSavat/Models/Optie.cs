﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_LucSavat.Models
{
    public class Optie
    {
        //Properties
        public int Id { get; set; }
        public string Naam { get; set; }

        //Mapping Properties
        public List<Vehicle> Vehicles { get; set; }
    }
}