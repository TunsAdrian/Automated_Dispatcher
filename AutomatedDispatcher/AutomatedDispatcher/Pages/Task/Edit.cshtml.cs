using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class EditModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public EditModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Task Task { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
            ViewData["EmployeeId"] = new SelectList(_context.Employee, "Id", "FirstName");
            ViewData["StatusId"] = new SelectList(_context.Status, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Task).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
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
    }
}
