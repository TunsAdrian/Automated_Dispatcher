using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutomatedDispatcher.Data;
using Microsoft.AspNetCore.Http;
using AutomatedDispatcher.Repositories.Interfaces;

namespace AutomatedDispatcher.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly IEmployeeRepository _employeeRepository;

        public string Username { get; set; } // used for session

        public CreateModel(AutomatedDispatcher.Data.webappContext context, IEmployeeRepository employeeRepository)
        {
            _context = context;
            _employeeRepository = employeeRepository ?? throw new ArgumentNullException(nameof(employeeRepository));
        }

        public IActionResult OnGet()
        {
            Username = HttpContext.Session.GetString("username"); // establish session

            if (Username != null )
            {
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
        public Data.Employee Employee { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Set iniital current workload to 0
            Employee.CurrentWorkload = 0;

            Employee = await _employeeRepository.AddAsync(Employee);

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
