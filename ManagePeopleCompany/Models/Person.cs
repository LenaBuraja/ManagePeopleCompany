using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagePeopleCompany.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public int PositionID { get; set; }
        public double Experience { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public double? Pay { get; set; }
        public string Skype { get; set; }
        public Position Position { get; set; }
        public string FullName {
            get {
                return LastName + " " + FirstName + " " + MiddleName;
            }
        }
    }
}