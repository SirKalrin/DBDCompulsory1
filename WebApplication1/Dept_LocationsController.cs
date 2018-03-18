using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Dept_LocationsController : Controller
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: Dept_Locations
        public ActionResult Index()
        {
            var dept_Locations = db.Dept_Locations.Include(d => d.Department);
            return View(dept_Locations.ToList());
        }

        // GET: Dept_Locations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept_Locations dept_Locations = db.Dept_Locations.Find(id);
            if (dept_Locations == null)
            {
                return HttpNotFound();
            }
            return View(dept_Locations);
        }

        // GET: Dept_Locations/Create
        public ActionResult Create()
        {
            ViewBag.DNUmber = new SelectList(db.Department, "DNumber", "DName");
            return View();
        }

        // POST: Dept_Locations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DNUmber,DLocation")] Dept_Locations dept_Locations)
        {
            if (ModelState.IsValid)
            {
                db.Dept_Locations.Add(dept_Locations);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DNUmber = new SelectList(db.Department, "DNumber", "DName", dept_Locations.DNUmber);
            return View(dept_Locations);
        }

        // GET: Dept_Locations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept_Locations dept_Locations = db.Dept_Locations.Find(id);
            if (dept_Locations == null)
            {
                return HttpNotFound();
            }
            ViewBag.DNUmber = new SelectList(db.Department, "DNumber", "DName", dept_Locations.DNUmber);
            return View(dept_Locations);
        }

        // POST: Dept_Locations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DNUmber,DLocation")] Dept_Locations dept_Locations)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dept_Locations).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DNUmber = new SelectList(db.Department, "DNumber", "DName", dept_Locations.DNUmber);
            return View(dept_Locations);
        }

        // GET: Dept_Locations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dept_Locations dept_Locations = db.Dept_Locations.Find(id);
            if (dept_Locations == null)
            {
                return HttpNotFound();
            }
            return View(dept_Locations);
        }

        // POST: Dept_Locations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Dept_Locations dept_Locations = db.Dept_Locations.Find(id);
            db.Dept_Locations.Remove(dept_Locations);
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
