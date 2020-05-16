﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace AutomatedDispatcher.Pages.Task
{
    public class DetailsModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public DetailsModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

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
            return Page();
        }
    }
}
