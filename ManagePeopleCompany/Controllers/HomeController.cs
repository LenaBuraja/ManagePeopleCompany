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
        //PersonContext db = new PersonContext();
        MPC_Context db = new MPC_Context();

        public ActionResult Index()
        {
            var people = db.Persons.Include(p => p.Position);
            //ViewBag.People = db.People.Include("Position");
            //ViewBag.People = db.People.Include(p = > p.Position);
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