using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Web.Mvc;

namespace ManagePeopleCompany.Models
{
    public class MPC_Context : DbContext
    {
        public DbSet<Position> Positions { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Coming> Comings { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Connection> Connections { get; set; }
        public DbSet<Status> Statuses { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Answer> Answers { get; set; }


        public SelectList PositionsList { get; set; }
        public SelectList StatusesList { get; set; }
        public SelectList HRList { get; set; }
        public SelectList AnswersList { get; set; }
    }
}