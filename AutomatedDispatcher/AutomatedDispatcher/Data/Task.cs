using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public int? StatusId { get; set; }
        public string Description { get; set; }
        public int Priority { get; set; }
        public int ExpectedTime { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Status Status { get; set; }
        public virtual ICollection<TaskSkill> TaskSkill { get; set; }
    }
}
