using System;
using System.Collections.Generic;
using System.Data;
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
        public int Id { get; set; }

        public menuProgrammerModel(ITaskRepository taskRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
  
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();
        public async Task<IActionResult> OnGetAsync(int id)
        {

            Username = HttpContext.Session.GetString("username");

            Id = id;

            if (Username != null)
            {
                TaskList = await _taskRepository.GetTaskListByIdAsync(Id);
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