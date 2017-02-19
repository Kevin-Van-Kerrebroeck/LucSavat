using Project_LucSavat.Models;
using Project_LucSavat.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Net;

namespace Project_LucSavat.Controllers
{
    public class AdminController : Controller
    {
        //Fields
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            var vZoekertjes = db.Zoekertjes.Include(z => z.Vehicle);
            var vVehicles = db.Vehicles.Include(v => v.Opties);
            var vView = new AdminViewModel()
            {
                Zoekertjes = vZoekertjes.ToList(),
                Vehicles = vVehicles.ToList()
            };

            return View(vView);
        }

        /*********ZOEKERTJES**********/

        // GET: Zoekertjes/Create
        public ActionResult CreateZoekertje()
        {
            //ViewModel aanmaken
            ZoekertjeViewModel vView = new ZoekertjeViewModel();

            //SelectList van ViewModel opvullen voor gebruik van dropdown in de view
            vView.Vehicles = new SelectList(db.Vehicles, "Id", "Type");

            //Show
            return View(vView);
        }

        // POST: Zoekertjes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateZoekertje(ZoekertjeViewModel vZoekertje)
        {
            //Controle
            if (ModelState.IsValid)
            {
                //Nieuw Zoekertje Object aanmaken en gegevens die uit het ViewModel komen daarvoor gebruiken
                Zoekertje zoekertje = new Zoekertje()
                {
                    Title = vZoekertje.Zoekertje.Title,
                    Prijs = vZoekertje.Zoekertje.Prijs,
                    Beschrijving = vZoekertje.Zoekertje.Beschrijving,
                    Vehicle = db.Vehicles.Find(vZoekertje.Zoekertje.VehicleId)
                };

                //Zoekertje toevoegen aan db en wijzigingen opslaan
                db.Zoekertjes.Add(zoekertje);
                db.SaveChanges();
            }

            //Show
            return RedirectToAction("Index");
        }
        
        
        // GET: Zoekertjes/Edit/5
        public ActionResult EditZoekertje(int? id)
        {
            //Controlle Op ID
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Nieuw Zoekertje Object aanmaken, eager loading met Include voor vehicle (zodat een link naar het vehicle kan voorzien worden??)
            Zoekertje vZoekertje = db.Zoekertjes
                .Include(z => z.Vehicle)
                .SingleOrDefault(z => z.Id == id);

            //Controle op zoekertje
            if(vZoekertje == null)
            {
                return HttpNotFound();
            }

            //Zoekertje in ViewModel steken, Select List aanmaken
            ZoekertjeViewModel vView = new ZoekertjeViewModel()
            {
                Zoekertje = vZoekertje,
                Vehicles = new SelectList(db.Vehicles, "Id", "Type")
            };

            //Show
            return View(vView);
        }

        // POST: Zoekertjes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditZoekertje(ZoekertjeViewModel vZoekertje)
        {
            //Controle
            if (ModelState.IsValid)
            {
                //Controle
                if (vZoekertje.Zoekertje == null)
                {
                    return HttpNotFound();
                }

                //State van Zoekertje wijzigen en wijzigingen opslaan
                db.Entry(vZoekertje.Zoekertje).State = EntityState.Modified;
                db.SaveChanges();

                //Bevestigings boodschap meegeven
                ViewBag.Info = "Gegevens gewijzigd: " + DateTime.Now.ToShortTimeString();

                //Show
                vZoekertje.Vehicles = new SelectList(db.Vehicles, "Id", "Type");
                return View(vZoekertje);
            }

            return View(vZoekertje);
        }

        // POST: Zoekertjes/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteZoekertje(int? id)
        {
            //Controle
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Zoekertje vinden
            Zoekertje vZoekertje = db.Zoekertjes.Find(id);

            //zoekertje verwijderen en wijziging opslaan
            db.Zoekertjes.Remove(vZoekertje);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        /*********AUTOS**********/
        // GET: Vehicles/Create
        public ActionResult CreateVehicle()
        {
            //ViewModel aanmaken
            VehicleViewModel vView = new VehicleViewModel();

            //SelectList van ViewModel opvullen voor gebruik van dropdown in de view
            vView.Opties = new SelectList(db.Opties, "Id", "Naam");

            //Show
            return View(vView);
        }

        // POST: Vehicles/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateVehicle(VehicleViewModel vVehicle)
        {
            //Controle
            //if (ModelState.IsValid)
            //{
                //Nieuw Vehicle Object aanmaken en gegevens die uit het ViewModel komen daarvoor gebruiken
                Vehicle vehicle = new Vehicle();
                vehicle = vVehicle.Vehicle;

                //Vehicle toevoegen aan db en wijzigingen opslaan
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
            //}

            //Show
            return RedirectToAction("Index");
        }


        // GET: Vehicles/Edit/5
        public ActionResult EditVehicle(int? id)
        {
            //Controlle Op ID
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Nieuw Vehicle Object aanmaken, eager loading met Include voor vehicle (zodat een link naar het vehicle kan voorzien worden??)
            Vehicle vVehicle = db.Vehicles
                .Include(v => v.Opties)
                .SingleOrDefault(z => z.Id == id);

            //Controle op vehicle
            if (vVehicle == null)
            {
                return HttpNotFound();
            }

            //Vehicle in ViewModel steken, Select List aanmaken
            VehicleViewModel vView = new VehicleViewModel()
            {
                Vehicle = vVehicle,
                Opties = new SelectList(db.Opties, "Id", "Naam")
            };

            //Show
            return View(vView);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(VehicleViewModel vVehicle)
        {
            //Controle
            if (ModelState.IsValid)
            {
                //Controle
                if (vVehicle.Vehicle == null)
                {
                    return HttpNotFound();
                }

                //State van Vehicle wijzigen en wijzigingen opslaan
                db.Entry(vVehicle.Vehicle).State = EntityState.Modified;
                db.SaveChanges();

                //Bevestigings boodschap meegeven
                ViewBag.Info = "Gegevens gewijzigd: " + DateTime.Now.ToShortTimeString();

                //Show
                vVehicle.Opties = new SelectList(db.Opties, "Id", "Naam");
                return View(vVehicle);
            }

            return View(vVehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteVehicle(int? id)
        {
            //Controle
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Zoekertje vinden
            Vehicle vVehicle = db.Vehicles.Find(id);

            //zoekertje verwijderen en wijziging opslaan
            db.Vehicles.Remove(vVehicle);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        /*********OPTIES**********/

        /***********EXTRAS************/
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}