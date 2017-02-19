using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project_LucSavat.Models;

namespace Project_LucSavat.Controllers
{
    public class ZoekertjesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Zoekertjes
        public ActionResult Index()
        {
            var zoekertje = db.Zoekertje.Include(z => z.Vehicle);
            return View(zoekertje.ToList());
        }

        // GET: Zoekertjes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zoekertje zoekertje = db.Zoekertje.Find(id);
            if (zoekertje == null)
            {
                return HttpNotFound();
            }
            return View(zoekertje);
        }

        // GET: Zoekertjes/Create
        public ActionResult Create()
        {
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Type");
            return View();
        }

        // POST: Zoekertjes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Prijs,Beschrijving,VehicleId")] Zoekertje zoekertje)
        {
            if (ModelState.IsValid)
            {
                db.Zoekertje.Add(zoekertje);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Type", zoekertje.VehicleId);
            return View(zoekertje);
        }

        // GET: Zoekertjes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zoekertje zoekertje = db.Zoekertje.Find(id);
            if (zoekertje == null)
            {
                return HttpNotFound();
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Type", zoekertje.VehicleId);
            return View(zoekertje);
        }

        // POST: Zoekertjes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Prijs,Beschrijving,VehicleId")] Zoekertje zoekertje)
        {
            if (ModelState.IsValid)
            {
                db.Entry(zoekertje).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.VehicleId = new SelectList(db.Vehicles, "Id", "Type", zoekertje.VehicleId);
            return View(zoekertje);
        }

        // GET: Zoekertjes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zoekertje zoekertje = db.Zoekertje.Find(id);
            if (zoekertje == null)
            {
                return HttpNotFound();
            }
            return View(zoekertje);
        }

        // POST: Zoekertjes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zoekertje zoekertje = db.Zoekertje.Find(id);
            db.Zoekertje.Remove(zoekertje);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

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
