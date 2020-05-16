using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AutomatedDispatcher.Pages.Employee
{
    public class IndexModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _context;

        public IndexModel(AutomatedDispatcher.Data.webappContext context)
        {
            _context = context;
        }

        public IList<Data.Employee> Employee { get; set; }

        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Employee = await _context.Employee.ToListAsync();
        }
    }
}
