using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutomatedDispatcher.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

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
        public string Username { get; set; }
        [BindProperty, Required]
        public string Password { get; set; }


        public void OnGet()
        {

        }

        public IActionResult OnPost()
        {
            webappContext context = new webappContext();


            var login = from employee in context.Employee
                        where employee.Username.Equals(Username) == true && employee.Password.Equals(Password) == true
                        select employee; 

            if (ModelState.IsValid == false)
            {
                return Page();
            }
            else
            {
                //return RedirectToPage("/Manager/meniuManager");

                
                foreach (var i in login)
                {
                    if ( i.Role == 0 )
                    {
                        HttpContext.Session.SetString("username", Username);
                        return RedirectToPage("/Manager/menuManager", new { Logged = true} );

                    } else if ( i.Role  == 1 )
                    {
                        HttpContext.Session.SetString("username", Username);
                        return RedirectToPage("/Programmer/menuProgrammer", new { Logged = true , Id = i.Id});
                    } 

                }


                return Page();

            }
        }
    }
}
