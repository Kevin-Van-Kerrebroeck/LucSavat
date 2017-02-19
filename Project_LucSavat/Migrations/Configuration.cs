namespace Project_LucSavat.Migrations
{
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Project_LucSavat.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(Project_LucSavat.Models.ApplicationDbContext context)
        {
            //1. Maak een lijst van Opties
            var Opties = new List<Optie>()
            {
                new Optie() {Naam="Airco" },
                new Optie() {Naam="Electrische ramen" },
                new Optie() {Naam="Radio" },
                new Optie() {Naam="Trekhaak" }
            };

            //2. Lijst Opslaan
            context.Opties.AddOrUpdate(o => o.Naam, Opties.ToArray());

            //3. Maak een lijst van auto's
            var Autos = new List<Vehicle>()
            {
                new Vehicle() {
                    Merk = Merken.Audi,
                    Type ="A3",
                    EersteInschrijving = new DateTime(2001,1,1),
                    Brandstof = Brandstof.Diesel,
                    KmStand = 12000,
                    Transmissie = TransmissieType.Manueel,
                    AantalDeuren = 5,
                    Kleur = "rood"
                },
                new Vehicle() {
                    Merk = Merken.Renault,
                    Type ="Clio",
                    EersteInschrijving = new DateTime(2002,1,1),
                    Brandstof = Brandstof.Benzine,
                    KmStand = 12000,
                    Transmissie = TransmissieType.Manueel,
                    AantalDeuren = 5,
                    Kleur = "Blauw"
                },
                new Vehicle() {
                    Merk = Merken.Honda,
                    Type ="Civic",
                    EersteInschrijving = new DateTime(1998,1,1),
                    Brandstof = Brandstof.Benzine,
                    KmStand = 120000,
                    Transmissie = TransmissieType.Manueel,
                    AantalDeuren = 5,
                    Kleur = "groen"
                },
            };

            // 4. lijst opslaan
            context.Vehicles.AddOrUpdate(v => v.Type, Autos.ToArray());
        }
    }
}
