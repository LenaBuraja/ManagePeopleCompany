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
    public class PeopleController : Controller
    {
        private MPC_Context db = new MPC_Context();

        // GET: People
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

        public ActionResult GetPeople(int? filterPosition = null, string filterFN = null, string filterLN = null)
        {
            IQueryable<Person> people = db.Persons;
            if (filterPosition != null && filterPosition >= 0)
            {
                people = people.Where(c => c.PositionID == filterPosition);
            }
            if (filterFN != null)
            {
                people = people.Where(c => c.FirstName.Contains(filterFN));
            }

            if (filterLN != null)
            {
                people = people.Where(c => c.LastName.Contains(filterLN));
            }

            return View(people.ToList());
        }

        // GET: People/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: People/Create
        public ActionResult Create()
        {
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "TitlePosition");
            return View();
        }

        // POST: People/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,MiddleName,PositionID,Experience,Phone,Email,Pay,Skype")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Persons.Add(person);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.PositionID = new SelectList(db.Positions, "Id", "TitlePosition", person.PositionID);
            return View(person);
        }

        // GET: People/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "TitlePosition", person.PositionID);
            return View(person);
        }

        // POST: People/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,MiddleName,PositionID,Experience,Phone,Email,Pay,Skype")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "TitlePosition", person.PositionID);
            return View(person);
        }

        // GET: People/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = db.Persons.Find(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: People/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Person person = db.Persons.Find(id);
            db.Persons.Remove(person);
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
