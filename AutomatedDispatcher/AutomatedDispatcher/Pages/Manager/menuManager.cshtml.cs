using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Manager
{
    public class menuManagerModel : PageModel
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;


        [BindProperty]
        public int Number { get; set; } // used for getting the values of dropdown menu


        public string Username { get; set; } // used for session
        

        public menuManagerModel(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();

        public IEnumerable<Data.Employee> EmployeeList { get; set; } = new List<Data.Employee>();

        public async Task<IActionResult> OnGetAsync()
        {
            Username = HttpContext.Session.GetString("username"); // establish session

            if (Username != null) 
            {
               
                TaskList = await _taskRepository.GetTaskListAsync();
                EmployeeList = await _employeeRepository.GetProgrammersListAsync();
                return Page();

            }
            else
            {
                return RedirectToPage("../Index");
            }
           
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }

        public void OnPost()
        {
            // posted value is assigned to the Number property automatically
        }
    }
}
