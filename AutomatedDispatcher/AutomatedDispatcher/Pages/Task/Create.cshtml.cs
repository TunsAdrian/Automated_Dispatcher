using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class CreateModel : PageModel 
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public CreateModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // When a task is created it should not be assigned to anyone
            Task.EmployeeId = null;

            // When a task is created it should have an unassigned status
            Task.StatusId = 2;

            // Set StartDate to create time   
            Task.StartDate = DateTime.Now;

            _context.Task.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
