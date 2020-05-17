using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutomatedDispatcher.Pages.Programmer
{
    public class menuProgrammerModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public menuProgrammerModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
  
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();
        public async Task<IActionResult> OnGetAsync(Boolean Logged)
        {

            Username = HttpContext.Session.GetString("username");

            if (Username.Equals(null) == false)
            {
                TaskList = await _taskRepository.GetTaskListAsync();
                return Page();
            } else

            {
                return RedirectToPage("../Index");
            }
         
            
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }

    }
}