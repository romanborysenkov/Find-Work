using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FindWorkRazor.Pages.Employer
{
    public class MyVacanciesModel : PageModel
    {


        private readonly SignInManager<IdentityUser> signinManager;

       
        public User employer { get; set; }

        private readonly DataContext _context;

        [BindProperty]
      public  List<Vacancy> vacancies { get; set; }
      

       public MyVacanciesModel(DataContext context, SignInManager<IdentityUser> signInManager)
        {
            signinManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (signinManager.IsSignedIn(User))
                employer = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
            else return RedirectToPage("/Index");

            vacancies = await _context.vacancies.Where(x=>x.userId == employer.Id).ToListAsync();

            return Page();

        }
    }
}
