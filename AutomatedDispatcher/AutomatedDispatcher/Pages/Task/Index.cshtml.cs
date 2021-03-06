﻿using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutomatedDispatcher.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public IndexModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        public IList<Data.Task> Task { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Task = await _context.Task.ToListAsync();

            // Not sure about this piece of code. Is from the old project.

            //Task = await _context.Task
            //    .Include(t => t.Employee)
            //    .Include(t => t.Status).ToListAsync();
        }
    }
}
