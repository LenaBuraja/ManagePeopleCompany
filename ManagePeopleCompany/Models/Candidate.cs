using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ManagePeopleCompany.Models
{
    public class Candidate
    {
        public int Id { get; set; }
        public int PersonID { get; set; }
        public int EmployeeID { get; set; }
        public int? StatusID { get; set; }
        public int? ConnectionID { get; set; }
        public int ComingID { get; set; }
        public DateTime? DateInterview { get; set; }
        public int? DecisionTime { get; set; }
        public int? AnswerID { get; set; }


        public virtual Coming Coming { get; set; }
        public virtual Connection Connection { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual Person Person { get; set; }
        public virtual Answer Answer { get; set; }

    }
}