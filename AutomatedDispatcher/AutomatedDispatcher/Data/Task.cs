using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutomatedDispatcher.Data
{
    public partial class Task
    {
        public Task()
        {
            TaskSkill = new HashSet<TaskSkill>();
        }

        public int Id { get; set; }
        public int? EmployeeId { get; set; }

        [Required]
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public string Description { get; set; }

        [Required]
        [Range(1, 10, ErrorMessage = "Alege o valoare intre 1 si 10")]
        public int Priority { get; set; }

        [Required]
        public int ExpectedTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<TaskSkill> TaskSkill { get; set; }
    }
}
