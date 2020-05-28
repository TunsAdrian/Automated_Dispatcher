using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using AutomatedDispatcher.Repositories.Interfaces;

namespace AutomatedDispatcher.Pages.Programmer
{
    public class TaskRefusedModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public TaskRefusedModel(AutomatedDispatcher.Data.webappContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [BindProperty]
        public Data.Task Task { get; set; }
        public Data.Employee Employee { get; set; }
        public IEnumerable<Data.Employee> CandidateProgrammers { get; set; } = new List<Data.Employee>();

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
                // The employee that rejects a task should have his Current Workload updated
                Employee = await _employeeRepository.GetEmployeeByIdAsync(Task.EmployeeId.Value);
                Employee.CurrentWorkload -= Task.ExpectedTime;

                // Get the candidate programmers suitable for the task and the best candidate for it
                CandidateProgrammers = await _employeeRepository.GetProgrammersMinWorkload(Employee.Id);
                var bestCandidate = CandidateProgrammers.Cast<Data.Employee>().First();

                // Update data for assigned programmer
                Task.EmployeeId = bestCandidate.Id;
                bestCandidate.CurrentWorkload += Task.ExpectedTime;

                await _context.SaveChangesAsync();
            }
            return RedirectToPage("../Programmer/menuProgrammer/");
        }
    }
}