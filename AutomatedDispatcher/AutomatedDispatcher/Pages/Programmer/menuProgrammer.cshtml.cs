using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomatedDispatcher.Pages.Programmer
{
    public class menuProgrammerModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public menuProgrammerModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
  
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();
        public async Task<IActionResult> OnGetAsync(Boolean Logged)
        {
            if(Logged == true )
            {
                TaskList = await _taskRepository.GetTaskListAsync();
                return Page();
            } else
            {
                return RedirectToPage("../Index");
            }
         
            
        }
    }
}