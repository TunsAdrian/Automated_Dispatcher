using System;
using System.Collections.Generic;

namespace AutomatedDispatcher.Data
{
    public partial class Employee
    {
        public Employee()
        {
            EmployeeSkill = new HashSet<EmployeeSkill>();
            Task = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public short Role { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int WorkingHours { get; set; }
        public int? CurrentWorkload { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkill { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}
