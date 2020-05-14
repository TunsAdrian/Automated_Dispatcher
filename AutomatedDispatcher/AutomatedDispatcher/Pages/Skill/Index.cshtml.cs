using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using AutomatedDispatcher.Data;

namespace AutomatedDispatcher.Pages.Skill
{
    public class IndexModel : PageModel
    {
        private readonly AutomatedDispatcher.Data.webappContext _skillRepository;

        public IndexModel(AutomatedDispatcher.Data.webappContext skillRepository)
        {
            _skillRepository = skillRepository ?? throw new ArgumentNullException(nameof(skillRepository));
        }

        //public IEnumerable<Data.Skill> SkillList { get; set; } = new List<Data.Skill>();
        //[BindProperty(SupportsGet = true)]
        public IList<Data.Skill> Skill { get;set; }

        // TODO: Modify here
        public async System.Threading.Tasks.Task OnGetAsync()
        {
            Skill = await _skillRepository.Skill.ToListAsync();
        }
    }
}
