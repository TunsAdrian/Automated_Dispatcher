using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutomatedDispatcher.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

using Microsoft.Extensions.Logging;

namespace AutomatedDispatcher.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        [Required, BindProperty]
        public AutomatedDispatcher.Data.Employee user { get; set; }


        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            webappContext context = new webappContext();


            var login = from employee in context.Employee
                        where employee.Username.Equals(user.Username) == true && employee.Password.Equals(user.Password) == true
                        select employee; 

            if (ModelState.IsValid == false)
            {
                return Page();
            }
            else
            {
                //   return RedirectToPage("/Manager/menuManager", new { Logged = true} );


                foreach (var i in login)
                {
                    if (i.Role == 0)
                    {
                        HttpContext.Session.SetString("username", user.Username);
                        return RedirectToPage("/Manager/menuManager");


                    }
                    else if (i.Role == 1)
                    {
                        HttpContext.Session.SetString("username", user.Username);
                        return RedirectToPage("/Programmer/menuProgrammer");
                    }

                }

                return Page();

            }
        }
    }
}
