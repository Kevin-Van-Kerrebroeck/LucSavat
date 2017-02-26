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
            var vOpties = db.Opties;
            var vView = new AdminViewModel()
            {
                Zoekertjes = vZoekertjes.ToList(),
                Vehicles = vVehicles.ToList(),
                Opties = vOpties.ToList()
            };

            return View(vView);
        }

        /*********ZOEKERTJES**********/
        #region Zoekertjes
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
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            //Nieuw Zoekertje Object aanmaken, eager loading met Include voor vehicle (zodat een link naar het vehicle kan voorzien worden??)
            Zoekertje vZoekertje = db.Zoekertjes
                .Include(z => z.Vehicle)
                .SingleOrDefault(z => z.Id == id);

            //Controle op zoekertje
            if (vZoekertje == null)
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
        #endregion

        /*********AUTOS**********/
        #region Vehicles
        // GET: Vehicles/Create
        public ActionResult CreateVehicle()
        {
            //ViewModel aanmaken
            VehicleViewModel vView = new VehicleViewModel();

            //Lijst van OptieCheckBoxViewModel opvullen met de gegevens uit Optie om alle opties te kunnen weergeven
            foreach (var o in db.Opties)
            {
                Optie Optie = db.Opties.Find(o.Id);
                vView.OptiesCheckBox.Add(new OptieCheckBoxViewModel()
                {
                    Id = o.Id,
                    Naam = o.Naam,
                    Checked = false
                });
            }

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
            {
                //Nieuw Vehicle Object aanmaken en gegevens die uit het ViewModel komen daarvoor gebruiken
                Vehicle vehicle = new Vehicle();
                vehicle = vVehicle.Vehicle;
                foreach (var o in vVehicle.OptiesCheckBox)
                {
                    if (o.Checked)
                    {
                        Optie vOptie = db.Opties.Find(o.Id);
                        vehicle.Opties.Add(vOptie);
                    }

                }


                //Vehicle toevoegen aan db en wijzigingen opslaan
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
            }

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

            //Nieuw Vehicle Object aanmaken, eager loading met Include voor Opties
            Vehicle vVehicle = db.Vehicles
                .Include(v => v.Opties)
                .SingleOrDefault(v => v.Id == id);

            //Controle op vehicle
            if (vVehicle == null)
            {
                return HttpNotFound();
            }

            //Vehicle in ViewModel steken
            VehicleViewModel vView = new VehicleViewModel()
            {
                Vehicle = vVehicle

            };
            //Checkbox lijst opvullen
            foreach (var o in db.Opties)
            {
                vView.OptiesCheckBox.Add(new OptieCheckBoxViewModel()
                {
                    Id = o.Id,
                    Naam = o.Naam,
                    Checked = (vView.Vehicle.Opties.Contains(o) ? true : false)
                });
            }

            //foreach (var o in vView.Vehicle.Opties)
            //{
            //    int vId = o.Id;
            //    vView.OptiesCheckBox.Find(v => v.Id == vId).Checked = true;
            //}
            //Show
            return View(vView);
        }

        // POST: Vehicles/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditVehicle(VehicleViewModel vVehicle)
        {
            //Source:http://stackoverflow.com/a/14460197/2715331 "Updating many to many relationships"

            //Controle op model state
            if (ModelState.IsValid)
            {


                //Ophalen van Entry in Context (db) aan de hand van vVehicle
                Vehicle vehicle = db.Vehicles
                    .Include(v => v.Opties)
                    .SingleOrDefault(v => v.Id == vVehicle.Vehicle.Id);

                //controle
                if (vehicle == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                //Opties Uit de checkbox lijst toevoegen of verwijderen aan het opgehaalde object
                foreach (var optie in vVehicle.OptiesCheckBox)
                {
                    Optie o = db.Opties.Find(optie.Id);
                    bool hasOptie = vehicle.Opties.Contains(o);
                    bool isChecked = optie.Checked;

                    if (isChecked && !hasOptie)
                    {
                        vehicle.Opties.Add(o);
                    }
                    else if (!isChecked && hasOptie)
                    {
                        vehicle.Opties.Remove(o);
                    }
                }

                //wijzigingen opslaan
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(vVehicle);
            #region code from SO
            //var vehicleX = db.Entry(vVehicle.Vehicle);
            //vehicleX.State = EntityState.Modified;
            //vehicleX.Collection(v => v.Opties).Load();

            //foreach (var o in vVehicle.OptiesCheckBox)
            //{
            //    if (o.Checked)
            //    {
            //        var optie = db.Opties.Find(o.Id);
            //        vVehicle.Vehicle.Opties.Add(optie);
            //    }else
            //    {
            //        var optie = db.Opties.Find(o.Id);
            //        vVehicle.Vehicle.Opties.Remove(optie);
            //    }

            //}

            //db.SaveChanges();
            //return RedirectToAction("Index");
            #endregion

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
        #endregion

        /*********OPTIES**********/
        #region Opties
        public ActionResult CreateOptie()
        {
            //Show
            return View();
        }

        // POST: Opties/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateOptie(Optie vOptie)
        {
            //Controle
            if (ModelState.IsValid)
            {
                //Optie toevoegen aan db en wijzigingen opslaan
                db.Opties.Add(vOptie);
                db.SaveChanges();
            }

            //Show
            return RedirectToAction("Index");
        }


        // GET: Opties/Edit/5
        public ActionResult EditOptie(int? id)
        {
            //Controle Op ID
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Optie vOptie = db.Opties.Find(id);

            //Controle op object
            if (vOptie == null)
            {
                return HttpNotFound();
            }

            //Show
            return View(vOptie);
        }

        // POST: Opties/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditOptie(Optie vOptie)
        {
            //Controle op state
            if (ModelState.IsValid)
            {
                //Entity markeren als modified
                db.Entry(vOptie).State = EntityState.Modified;

                //wijzigingen opslaan
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(vOptie);

        }

        // POST: Opties/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteOptie(int? id)
        {
            //Controle
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Optie vinden
            Optie vOptie = db.Opties.Find(id);

            //Optie verwijderen en wijziging opslaan
            db.Opties.Remove(vOptie);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        #endregion
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