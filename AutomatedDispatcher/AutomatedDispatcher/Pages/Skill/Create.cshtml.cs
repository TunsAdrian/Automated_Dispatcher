using AutomatedDispatcher.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Skill
{
    public class CreateModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;
        private readonly ISkillRepository _skillRepository;

        public string Username { get; set; } // used for session

        public CreateModel(AutomatedDispatcher.Data.webappContext context, ISkillRepository skillRepository)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        }

        //public IActionResult OnGet()
        //{
        //    return Page();
        //}

        [BindProperty]
        public Data.Skill Skill { get; set; }

        public IEnumerable<Data.Skill> SkillList { get; set; } = new List<Data.Skill>();

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                _context.Skill.Add(Skill);
                await _context.SaveChangesAsync();
            }
            catch
            {
                return RedirectToPage("Create");
            }
            return RedirectToPage("Create");
        }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Username = HttpContext.Session.GetString("username"); // establish session
            if (Username != null)
            {
                // Get skill list
                SkillList = await _context.Skill.ToListAsync();
            } 
        }

        public IActionResult OnGetLogout()
        {
            HttpContext.Session.Remove("username");
            return RedirectToPage("../Index");
        }

    }
}
