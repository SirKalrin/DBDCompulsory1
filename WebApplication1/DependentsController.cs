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
    public class DependentsController : Controller
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: Dependents
        public ActionResult Index()
        {
            var dependent = db.Dependent.Include(d => d.Employee);
            return View(dependent.ToList());
        }

        // GET: Dependents/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependent dependent = db.Dependent.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            return View(dependent);
        }

        // GET: Dependents/Create
        public ActionResult Create()
        {
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName");
            return View();
        }

        // POST: Dependents/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Essn,Dependent_Name,Sex,BDate,Relationship")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                db.Dependent.Add(dependent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", dependent.Essn);
            return View(dependent);
        }

        // GET: Dependents/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependent dependent = db.Dependent.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", dependent.Essn);
            return View(dependent);
        }

        // POST: Dependents/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Essn,Dependent_Name,Sex,BDate,Relationship")] Dependent dependent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(dependent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", dependent.Essn);
            return View(dependent);
        }

        // GET: Dependents/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Dependent dependent = db.Dependent.Find(id);
            if (dependent == null)
            {
                return HttpNotFound();
            }
            return View(dependent);
        }

        // POST: Dependents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Dependent dependent = db.Dependent.Find(id);
            db.Dependent.Remove(dependent);
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
