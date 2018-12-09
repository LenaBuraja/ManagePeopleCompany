using ManagePeopleCompany.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ManagePeopleCompany.Controllers
{
    public class ListPeopleController : Controller
    {
        MPC_Context db = new MPC_Context();


        public ActionResult Index()
        {
            return View(db.Persons.ToList<Person>());
        }

        public List<Person> GetListPerson()
        {
            return db.Persons.ToList<Person>();
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}