using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using AutomatedDispatcher.Data;

namespace AutomatedDispatcher.Pages.Task
{
    public class CreateModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public CreateModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
        ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName");
        ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
            return Page();
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

            _context.Task.Add(Task);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
