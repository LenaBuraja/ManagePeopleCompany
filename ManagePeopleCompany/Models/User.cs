using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ManagePeopleCompany.Models
{
    public class User
    {
        public int Id { get; set; }
        public int EmployeeID { get; set; }
        public string Login { get; set; }
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Confirm password and password do not match")]
        public virtual string ConfirmPassword { get; set; }
        public virtual string Email { get; set; }
        public DateTime DateRegistration { get; set; }
        public Guid ActivationCode { get; set; }
        public bool IsEmailVerified { get; set; }
        public bool RememberMe { get; set; }

        public virtual Employee Employee { get; set; }
    }
}