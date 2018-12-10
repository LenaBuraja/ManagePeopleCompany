using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ManagePeopleCompany.Models;

namespace ManagePeopleCompany.Controllers
{
    public class EmployeeController : Controller
    {
        private MPC_Context db = new MPC_Context();

        // GET: Employees
        public ActionResult Index()
        {
            Position addPosition = new Position { TitlePosition = "Все", Id = -1 };
            List<Position> positionList = db.Positions.ToList();
            positionList.Add(addPosition);
            ViewBag.filterPosition = positionList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int filterPosition, string filterFN, string filterLN)
        {
            Position addPosition = new Position { TitlePosition = "Все", Id = -1 };
            List<Position> positionList = db.Positions.ToList();
            positionList.Add(addPosition);
            ViewBag.filterPosition = positionList;
            ViewBag.filterFN = filterFN;
            ViewBag.filterLN = filterLN;
            return View();
        }

        public ActionResult GetEmployees(int? filterPosition = null, string filterFN = null, string filterLN = null)
        {
            var employees = db.Employees.Include(c => c.Person);
            if (filterPosition != null && filterPosition >= 0)
            {
                employees = employees.Where(c => c.Person.PositionID == filterPosition);
            }
            if (filterFN != null)
            {
                employees = employees.Where(c => c.Person.FirstName.Contains(filterFN));
            }

            if (filterLN != null)
            {
                employees = employees.Where(c => c.Person.LastName.Contains(filterLN));
            }

            return View(employees.ToList());
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
            return View();
        }

        // POST: Employees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Person,DateEnrollment")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Employees.Add(employee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

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
            List<Person> allPersonsEmployee = new List<Person>();
            var employees = (db.Employees).ToList();
            foreach (var item in employees)
            {
                allPersonsEmployee.Add(item.Person);
            }
            List<Person> persons = ((db.Persons).ToList().Except(allPersonsEmployee)).ToList();
            ViewBag.FreePersonList = new SelectList(persons, "Id", "FullName");

            return View(employee);
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Person,DateEnrollment")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
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
