using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Employee
{
    public class DeleteModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public DeleteModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Employee Employee { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
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
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Employee = await _context.Employee.FindAsync(id);

            if (Employee != null)
            {
                _context.Employee.Remove(Employee);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
