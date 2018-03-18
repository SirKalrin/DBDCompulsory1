using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1
{
    public class DepartmentsController : Controller
    {
        private CompanyEntities db = new CompanyEntities();

        // GET: Departments
        public ActionResult Index()
        {
            var result = db.usp_GetAllDepartments();
            List<Department> departments = new List<Department>();
            foreach (usp_GetAllDepartments_Result item in result)
            {
                departments.Add(new Department() { DName = item.DName, DNumber = item.DNumber, MgrStartDate = item.MgrStartDate, MgrSSN = item.MgrSSN, EmployeeCount = item.C_Employees.Value });       
            }
            //var department = db.Department.Include(d => d.Employee);
            return View(departments);
        }

        // GET: Departments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var result = db.usp_GetDepartment(id.Value);
            if (result == null)
            {
                return HttpNotFound();
            }
            Department d = null;
            foreach (usp_GetDepartment_Result item in result)
            {
                d = new Department() { DName = item.DName, DNumber = item.DNumber, MgrStartDate = item.MgrStartDate, MgrSSN = item.MgrSSN };
            }
            return View(d);
        }

        // GET: Departments/Create
        public ActionResult Create()
        {
            ViewBag.MgrSSN = new SelectList(db.Employee, "SSN", "FName");
            return View();
        }

        // POST: Departments/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DName,DNumber,MgrSSN,MgrStartDate")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.usp_CreateDepartment(department.DName, department.MgrSSN);
                return RedirectToAction("Index");
            }

            ViewBag.MgrSSN = new SelectList(db.Employee, "SSN", "FName", department.MgrSSN);
            return View(department);
        }

        // GET: Departments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            ViewBag.MgrSSN = new SelectList(db.Employee, "SSN", "FName", department.MgrSSN);
            return View(department);
        }

        // POST: Departments/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DName,DNumber,MgrSSN")] Department department)
        {
            if (ModelState.IsValid)
            {
                db.usp_UpdateDepartment(department.DNumber, department.DName);
                db.usp_UpdateDepartmentManager(department.DNumber, department.MgrSSN);
                return RedirectToAction("Index");
            }
            ViewBag.MgrSSN = new SelectList(db.Employee, "SSN", "FName", department.MgrSSN);
            return View(department);
        }

        // GET: Departments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Department department = db.Department.Find(id);
            if (department == null)
            {
                return HttpNotFound();
            }
            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.usp_DeleteDepartment(id);
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
