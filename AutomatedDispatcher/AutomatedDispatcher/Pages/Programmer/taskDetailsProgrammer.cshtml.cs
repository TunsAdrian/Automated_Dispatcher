using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutomatedDispatcher.Data;

namespace AutomatedDispatcher.Pages.Programmer
{
    public class taskDetailsProgrammerModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public taskDetailsProgrammerModel(AutomatedDispatcher.Data.webappContext context)
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
