using ManagePeopleCompany.Models;
using System.Data.Entity;
using System.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;

namespace ManagePeopleCompany.Controllers
{
    public class HomeController : Controller
    {
        MPC_Context db = new MPC_Context();

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
            IQueryable<Person> people = db.Persons.Include(p => p.Position);
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
            Person s = people.Where(c => c.Id == 0).FirstOrDefault();
            return View(people.ToList());
        }

        public ActionResult CreatePerson()
        {
            ViewBag.PositionID = new SelectList(db.Positions, "Id", "TitlePosition");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreatePerson([Bind(Include = "Id,FirstName,LastName,MiddleName,PositionID,Experience,Phone,Email,Pay,Skype")] Person person)
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

        public ActionResult EditPeople(int? id)
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPeople([Bind(Include = "Id,FirstName,LastName,MiddleName,PositionID,Experience,Phone,Email,Pay,Skype")] Person person)
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
            db.Dispose();
            base.Dispose(disposing);
        }

    }
}