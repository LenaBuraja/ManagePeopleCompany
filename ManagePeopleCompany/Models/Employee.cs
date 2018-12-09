using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagePeopleCompany.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public DateTime DateEnrollment { get; set; }

        public virtual Person Person { get; set; }

    }
}