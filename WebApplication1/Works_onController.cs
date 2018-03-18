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
    public class Works_onController : Controller
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: Works_on
        public ActionResult Index()
        {
            var works_on = db.Works_on.Include(w => w.Employee).Include(w => w.Project);
            return View(works_on.ToList());
        }

        // GET: Works_on/Details/5
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Works_on works_on = db.Works_on.Find(id);
            if (works_on == null)
            {
                return HttpNotFound();
            }
            return View(works_on);
        }

        // GET: Works_on/Create
        public ActionResult Create()
        {
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName");
            ViewBag.Pno = new SelectList(db.Project, "PNumber", "PName");
            return View();
        }

        // POST: Works_on/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Essn,Pno,Hours")] Works_on works_on)
        {
            if (ModelState.IsValid)
            {
                db.Works_on.Add(works_on);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", works_on.Essn);
            ViewBag.Pno = new SelectList(db.Project, "PNumber", "PName", works_on.Pno);
            return View(works_on);
        }

        // GET: Works_on/Edit/5
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Works_on works_on = db.Works_on.Find(id);
            if (works_on == null)
            {
                return HttpNotFound();
            }
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", works_on.Essn);
            ViewBag.Pno = new SelectList(db.Project, "PNumber", "PName", works_on.Pno);
            return View(works_on);
        }

        // POST: Works_on/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Essn,Pno,Hours")] Works_on works_on)
        {
            if (ModelState.IsValid)
            {
                db.Entry(works_on).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Essn = new SelectList(db.Employee, "SSN", "FName", works_on.Essn);
            ViewBag.Pno = new SelectList(db.Project, "PNumber", "PName", works_on.Pno);
            return View(works_on);
        }

        // GET: Works_on/Delete/5
        public ActionResult Delete(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Works_on works_on = db.Works_on.Find(id);
            if (works_on == null)
            {
                return HttpNotFound();
            }
            return View(works_on);
        }

        // POST: Works_on/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            Works_on works_on = db.Works_on.Find(id);
            db.Works_on.Remove(works_on);
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
