using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class CreateModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public CreateModel(AutomatedDispatcher.Data.webappContext context, ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public string Username { get; set; } // used for session

        public IActionResult OnGet()
        {

            Username = HttpContext.Session.GetString("username"); // establish session

            if (Username != null)
            {

                ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName");
                ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
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

        [BindProperty]
        public Data.Task Task { get; set; }

        public IEnumerable<Data.Employee> CandidateProgrammers { get; set; } = new List<Data.Employee>();

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Get the candidate programmers suitable for the task and the best candidate for it
            CandidateProgrammers = await _employeeRepository.GetProgrammersMinWorkload(-1);
            var bestCandidate = CandidateProgrammers.Cast<Data.Employee>().First();

            // Update data for assigned programmer
            Task.EmployeeId = bestCandidate.Id;
            bestCandidate.CurrentWorkload += Task.ExpectedTime;

            // When a task is created it is automatically assigned to it should be "In Progress"
            Task.StatusId = 3;

            // Set StartDate to create time   
            Task.StartDate = DateTime.Now.ToString("dd-MMMM-yy HH:mm");

            // If not description set default
            if (Task.Description == null) Task.Description = "-";

            Task = await _taskRepository.AddAsync(Task);

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
