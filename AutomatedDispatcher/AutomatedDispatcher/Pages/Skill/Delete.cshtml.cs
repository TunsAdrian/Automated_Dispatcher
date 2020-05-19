using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Skill
{
    public class DeleteModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public DeleteModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [BindProperty]
        public Data.Skill Skill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Skill = await _context.Skill.FirstOrDefaultAsync(m => m.Id == id);

            if (Skill == null)
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

            Skill = await _context.Skill.FindAsync(id);

            if (Skill != null)
            {
                _context.Skill.Remove(Skill);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("../Manager/menuManager");
        }
    }
}
