using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using AutomatedDispatcher.Repositories.Interfaces;
using System;

namespace AutomatedDispatcher.Pages.Employee
{
    public class EditModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public EditModel(AutomatedDispatcher.Data.webappContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        [BindProperty]
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

                Employee = await _context.Employee.FirstOrDefaultAsync(m => m.Id == id);

                if (Employee == null)
                {
                    return NotFound();
                }
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

            //_context.Attach(Employee).State = EntityState.Modified;

            try
            {
                //await _context.SaveChangesAsync();
                await _employeeRepository.UpdateAsync(Employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeExists(Employee.Id))
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

        private bool EmployeeExists(int id)
        {
            return _context.Employee.Any(e => e.Id == id);
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }
    }
}
