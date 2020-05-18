using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Role can be either 0 for manager or 1 for employee")]
        public short Role { get; set; }

        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$", ErrorMessage = "The field Username should match the form example@domain.com")]
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public int WorkingHours { get; set; }
        public int? CurrentWorkload { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkill { get; set; }
        public virtual ICollection<Task> Task { get; set; }
    }
}
