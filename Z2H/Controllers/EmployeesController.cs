using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Z2H.Models;

namespace Z2H.Controllers
{
    public class EmployeesController : Controller
    {
        private HRContext db = new HRContext();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.Department).Include(e => e.Employee2).Include(e => e.Job);
            return View(employees.ToList());
        }

        // GET: Employees/Search/name
        public ActionResult Search(string name)
        {
            var employees = db.Employees.Include(e => e.Department).Include(e => e.Employee2).Include(e => e.Job)
                .Where(e => e.FirstName.StartsWith(name) || e.LastName.StartsWith(name));
            return View("Index", employees.ToList());
        }


        // GET: Employees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName");
            ViewBag.ManagerId = new SelectList(db.Employees, "EmployeeId", "FirstName");
            ViewBag.JobId = new SelectList(db.Jobs, "JobId", "JobTitle");
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeId,FirstName,LastName,Email,PhoneNumber,HireDate,JobId,Salary,CommissionPct,ManagerId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Employees, "EmployeeId", "FirstName", employee.ManagerId);
            ViewBag.JobId = new SelectList(db.Jobs, "JobId", "JobTitle", employee.JobId);
            return View(employee);
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Employees, "EmployeeId", "FirstName", employee.ManagerId);
            ViewBag.JobId = new SelectList(db.Jobs, "JobId", "JobTitle", employee.JobId);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeId,FirstName,LastName,Email,PhoneNumber,HireDate,JobId,Salary,CommissionPct,ManagerId,DepartmentId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentId = new SelectList(db.Departments, "DepartmentId", "DepartmentName", employee.DepartmentId);
            ViewBag.ManagerId = new SelectList(db.Employees, "EmployeeId", "FirstName", employee.ManagerId);
            ViewBag.JobId = new SelectList(db.Jobs, "JobId", "JobTitle", employee.JobId);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
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
