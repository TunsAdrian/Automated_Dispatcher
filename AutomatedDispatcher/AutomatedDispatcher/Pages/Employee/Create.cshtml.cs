using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutomatedDispatcher.Data;
using Microsoft.AspNetCore.Http;

namespace AutomatedDispatcher.Pages.Employee
{
    public class CreateModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public string Username { get; set; } // used for session

        public CreateModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
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

            _context.Employee.Add(Employee);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
