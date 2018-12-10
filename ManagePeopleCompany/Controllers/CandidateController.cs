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
    public class CandidateController : Controller
    {
        private MPC_Context db = new MPC_Context();

        public ActionResult Index()
        {
            Status addStatus = new Status { TitleStatus = "Все", Id = -1 };
            List<Status> statusList = db.Statuses.ToList();
            statusList.Add(addStatus);
            ViewBag.filterStatus = statusList;
            Position addPosition = new Position { TitlePosition = "Все", Id = -1 };
            List<Position> positionList = db.Positions.ToList();
            positionList.Add(addPosition);
            ViewBag.filterPosition = positionList;
            return View();
        }

        [HttpPost]
        public ActionResult Index(int filterStatus, int filterPosition, string filterFN, string filterLN)
        {
            Status addStatus = new Status { TitleStatus = "Все", Id = -1 };
            List<Status> statusList = db.Statuses.ToList();
            statusList.Add(addStatus);
            ViewBag.filterStatus = statusList;
            Position addPosition = new Position { TitlePosition = "Все", Id = -1 };
            List<Position> positionList = db.Positions.ToList();
            positionList.Add(addPosition);
            ViewBag.filterPosition = positionList;
            ViewBag.filterFN = filterFN;
            ViewBag.filterLN = filterLN;
            return View();
        }

        public ActionResult GetCandidates(int? filterStatus = null, int? filterPosition = null, string filterFN = null, string filterLN = null)
        {
            var candidates = db.Candidates.Include(c => c.Employee).Include(c => c.Coming).Include(c => c.Connection).Include(c => c.Status).Include(c => c.Person);
            if (filterStatus != null && filterStatus >= 0)
            {
                candidates = candidates.Where(c => c.StatusID == filterStatus);
            }
            if (filterPosition != null && filterPosition >= 0)
            {
                candidates = candidates.Where(c => c.Person.PositionID == filterPosition);
            }
            if (filterFN != null)
            {
                candidates = candidates.Where(c => c.Person.FirstName.Contains(filterFN));
            }

            if (filterLN != null)
            {
                candidates = candidates.Where(c => c.Person.LastName.Contains(filterLN));
            }

            return View(candidates.ToList());
        }

        // GET: Candidates/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Include(c => c.Employee).Include(c => c.Coming).Include(c => c.Connection).Include(c => c.Status).Include(c => c.Person).FirstOrDefault(t => t.Id == id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

    //    public ActionResult Download()
    //    {
    //        return new FileContentResult(byte[], contentType);
    //}

        // GET: Candidates/Create
        public ActionResult Create()
        {
            List<Person> PersonsList = new List<Person>();
            var employees = db.Employees.Where(p => p.Person.PositionID == db.Positions.Where(t => t.TitlePosition == "HR").FirstOrDefault().Id).ToList();
            foreach (var item in employees)
            {
                PersonsList.Add(item.Person);
            }
            ViewBag.HRList = new SelectList(PersonsList, "Id", "FullName");
            List<Person> allPersonsEmployee = new List<Person>();
            employees = (db.Employees).ToList();
            foreach (var item in employees)
            {
                allPersonsEmployee.Add(item.Person);
            }
            List<Person> allPersonsCandidate = new List<Person>();
            List<Candidate> candidates = (db.Candidates).ToList();
            foreach (var item in candidates)
            {
                allPersonsCandidate.Add(item.Person);
            }
            List<Person> persons = (((db.Persons).ToList().Except(allPersonsEmployee)).Except(allPersonsCandidate)).ToList();
            ViewBag.FreePersonList = new SelectList(persons, "Id", "FullName");
            ViewBag.StatusID = new SelectList(db.Statuses, "Id", "TitleStatus");
            ViewBag.ConnectionID = new SelectList(db.Connections, "Id", "TitleConnection");
            ViewBag.ComingID = new SelectList(db.Comings, "Id", "TitleComing");
            return View();
        }

        // POST: Candidates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,PersonID,EmployeeID,StatusID,ConnectionID,ComingID")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Candidates.Add(candidate);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidates/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,PersonID,EmployeeID,StatusID,ConnectionID,ComingID")] Candidate candidate)
        {
            if (ModelState.IsValid)
            {
                db.Entry(candidate).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(candidate);
        }

        // GET: Candidates/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidate candidate = db.Candidates.Find(id);
            if (candidate == null)
            {
                return HttpNotFound();
            }
            return View(candidate);
        }

        // POST: Candidates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidate candidate = db.Candidates.Find(id);
            db.Candidates.Remove(candidate);
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
