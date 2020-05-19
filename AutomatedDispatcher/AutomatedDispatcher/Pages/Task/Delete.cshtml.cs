using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class DeleteModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        public string Username { get; set; } // used for session

        public DeleteModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Data.Task Task { get; set; }

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

            } else
            {
                return RedirectToPage("../Index");
            }
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
                _context.Task.Remove(Task);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Manager/menuManager");
        }
        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }
    }
}
