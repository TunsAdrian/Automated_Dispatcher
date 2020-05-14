using System;
using System.Collections.Generic;

namespace AutomatedDispatcher.Data
{
    public partial class TaskSkill
    {
        public int TaskId { get; set; }
        public int SkillId { get; set; }
        public int MinRequiredLevel { get; set; }

        public virtual Skill Skill { get; set; }
        public virtual Task Task { get; set; }
    }
}
