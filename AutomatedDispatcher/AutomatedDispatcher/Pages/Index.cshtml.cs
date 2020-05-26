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

        //  public AutomatedDispatcher.Data.Employee user { get; set; }


        [Required, BindProperty]
        public string username { get; set; }
        [Required, BindProperty]
        public string password { get; set; }

        public void OnGet()
        {
            
        }

        public IActionResult OnPost()
        {
            webappContext context = new webappContext();


            var login = from employee in context.Employee
                        where employee.Username.Equals(username) == true && employee.Password.Equals(password) == true
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
                        HttpContext.Session.SetString("username", username);
                        return RedirectToPage("/Manager/menuManager");


                    }
                    else if (i.Role == 1)
                    {
                        HttpContext.Session.SetString("username", password);
                        HttpContext.Session.SetInt32("id", i.Id);
                        return RedirectToPage("/Programmer/menuProgrammer");
                    }

                }

                return Page();

            }
        }
    }
}
