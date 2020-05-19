using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Employee
{
    public class DetailsModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public DetailsModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

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
    }
}
