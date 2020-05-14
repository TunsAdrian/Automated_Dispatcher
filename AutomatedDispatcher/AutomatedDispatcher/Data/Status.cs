using System;
using System.Collections.Generic;

namespace AutomatedDispatcher.Data
{
    public partial class Status
    {
        public Status()
        {
            Task = new HashSet<Task>();
        }

        public int Id { get; set; }
        public string Status1 { get; set; }

        public virtual ICollection<Task> Task { get; set; }
    }
}
