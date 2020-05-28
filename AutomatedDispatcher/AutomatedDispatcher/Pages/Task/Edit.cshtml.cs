using AutomatedDispatcher.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class EditModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly ITaskRepository _taskRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public EditModel(AutomatedDispatcher.Data.webappContext context, ITaskRepository taskRepository, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _taskRepository = taskRepository ?? throw new ArgumentNullException(nameof(taskRepository));
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [BindProperty]
        public Data.Task Task { get; set; }
        public Data.Employee Employee { get; set; }
        public String employeeFullName { get; set; }

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
                   .FirstOrDefaultAsync(m => m.Id == id);

                if (Task == null)
                {
                    return NotFound();
                }
                Employee = await _employeeRepository.GetEmployeeByIdAsync(Task.EmployeeId.Value);
                employeeFullName = Employee.FirstName + " " + Employee.LastName;

                ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName");
                //ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
                return Page();
            } else
            {
                return RedirectToPage("../Index");
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            //_context.Attach(Task).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await _taskRepository.UpdateAsync(Task);
                
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TaskExists(Task.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("../Manager/menuManager");
        }

        private bool TaskExists(int id)
        {
            return _context.Task.Any(e => e.Id == id);
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }
    }
}
