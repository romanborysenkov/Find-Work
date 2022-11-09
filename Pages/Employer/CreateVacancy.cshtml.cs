using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace FindWorkRazor.Pages.Employer
{
    public class CreateVacancyModel : PageModel
    {

        private readonly DataContext _context;

        [BindProperty]
        public Vacancy vacancy { get; set; } = new Vacancy();

        public CreateVacancyModel(DataContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public User employer { get; set; } = new User();

        public async Task<IActionResult> OnPostAsync()
        {
           var employer = await _context.users.FirstAsync(s => s.email == User.Identity.Name);


            vacancy.userId = employer.Id;
            vacancy.publishtime = DateTime.Now;
            _context.vacancies.Add(vacancy);
            _context.SaveChangesAsync();

            return Page();
        }
    }
}
