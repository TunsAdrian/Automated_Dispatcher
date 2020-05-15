using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomatedDispatcher.Pages.Manager
{
    public class IndexModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;

        public IndexModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();

        public async Task<IActionResult> OnGetAsync()
        {
            TaskList = await _taskRepository.GetTaskListAsync();
            return Page();
        }
    }
}
