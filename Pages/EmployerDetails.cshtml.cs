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

namespace FindWorkRazor.Pages
{
    public class EmployerDetailsModel : PageModel
    {
        private readonly DataContext _context;

        private readonly SignInManager<IdentityUser> signinManager;

        public List<Vacancy> vacancies { get; set; }

        public EmployerDetailsModel(DataContext context, SignInManager<IdentityUser> signinManager)
        {
            this.signinManager = signinManager;
            _context = context;
        }

        public User employer { get; set; } = default!;

       public User worker = new User();

        public async Task<IActionResult> OnGetAsync(string id)
        {
            employer = await _context.users.FirstOrDefaultAsync(x => x.Id == id);
            if (signinManager.IsSignedIn(User))
            {
                worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
                vacancies = await _context.vacancies.Where(x=>x.userId==employer.Id).ToListAsync();
            }
            return Page();
        }
    }
}
