using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AutomatedDispatcher.Data
{
    public partial class Skill
    {
        public Skill()
        {
            EmployeeSkill = new HashSet<EmployeeSkill>();
            TaskSkill = new HashSet<TaskSkill>();
        }

        public int Id { get; set; }
        [Required]
        
        public string SkillName { get; set; }

        public virtual ICollection<EmployeeSkill> EmployeeSkill { get; set; }
        public virtual ICollection<TaskSkill> TaskSkill { get; set; }
    }
}
