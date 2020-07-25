using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Programmer
{
    public class TaskCompletedModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public TaskCompletedModel(AutomatedDispatcher.Data.webappContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [BindProperty]
        public Data.Task Task { get; set; }
        public Data.Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Username = HttpContext.Session.GetString("username"); // establish session

            if (Username != null)
            {

                if (id == null)
                {
                    return NotFound();
                }

                Task = await _context.Task
                    .Include(t => t.Employee)
                    .Include(t => t.Status).FirstOrDefaultAsync(m => m.Id == id);

                if (Task == null)
                {
                    return NotFound();
                }
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
        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Task = await _context.Task.FindAsync(id);

            if (Task != null)
            {
                // The employee that completes a task should have his Current Workload updated
                Employee = await _employeeRepository.GetEmployeeByIdAsync(Task.EmployeeId.Value);
                Employee.CurrentWorkload -= Task.ExpectedTime;

                // Put end date to current time for task
                Task.EndDate = DateTime.Now.ToString("dd-MMMM-yy HH:mm");

                // Change status to "Completed"
                Task.StatusId = 1;

                // Remove employee
                //Task.Employee = null; //not working

                await _context.SaveChangesAsync();
            }
            return RedirectToPage("../Programmer/menuProgrammer/");
        }
    }
}