using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutomatedDispatcher.Data;

namespace AutomatedDispatcher.Pages.Task
{
    public class IndexModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public IndexModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        public IList<Data.Task> Task { get;set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Task = await _context.Task
                .Include(t => t.Employee)
                .Include(t => t.Status).ToListAsync();
        }
    }
}
