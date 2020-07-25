using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Skill
{
    public class EditModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public EditModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public string Username { get; set; } // used for session

        [BindProperty]
        public Data.Skill Skill { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Username = HttpContext.Session.GetString("username"); // establish session
            if (Username != null)
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
            else
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

            _context.Attach(Skill).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SkillExists(Skill.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("Create");
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }

        private bool SkillExists(int id)
        {
            return _context.Skill.Any(e => e.Id == id);
        }
    }
}
