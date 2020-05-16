using AutomatedDispatcher.Repositories.Interfaces;
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

        public menuManagerModel(ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(taskRepository));
        }
        public IEnumerable<Data.Task> TaskList { get; set; } = new List<Data.Task>();

        public IEnumerable<Data.Employee> EmployeeList { get; set; } = new List<Data.Employee>();

        public async Task<IActionResult> OnGetAsync(Boolean logged)
        {
            if (logged == true) { 
                TaskList = await _taskRepository.GetTaskListAsync();
                EmployeeList = await _employeeRepository.GetProgrammersListAsync();
                return Page();
            }
            else
            {
                return RedirectToPage("../Index");
            }
           
        }
    }
}
