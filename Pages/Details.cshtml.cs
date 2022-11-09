using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;

using System;



namespace FindWorkRazor.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly DataContext _context;

        private readonly SignInManager<IdentityUser> signinManager;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public List<Vacancy> vacancies { get; set; }

        public DetailsModel(DataContext context, SignInManager<IdentityUser> signinManager)
        {
            this.signinManager = signinManager;
            _context = context;
        }

        public Vacancy Vacancy { get; set; } = default!;

       public User worker = new User();

        IQueryable<Responses> responsies { get; set; }

        public bool isResponded { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Vacancy = await _context.vacancies.FirstOrDefaultAsync(x => x.vacancyId == id);
            if (signinManager.IsSignedIn(User))
            {
                worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
                responsies = _context.responses.Where(x => x.WorkerId == worker.Id);

                foreach (var vac in responsies)
                {
                    if (vac.VacancyId == Vacancy.vacancyId)
                    {
                        isResponded = true;
                        break;
                    }
                }
            } 
            return Page();
        }

        public async Task OnPostVacanciesAsync(int? id)
        {
            Vacancy = await _context.vacancies.FirstOrDefaultAsync(x => x.vacancyId == id);



            if (signinManager.IsSignedIn(User))
            {
                worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
                responsies = _context.responses.Where(x => x.WorkerId == worker.Id);

                foreach (var vac in responsies)
                {
                    if (vac.VacancyId == Vacancy.vacancyId)
                    {
                        isResponded = true;
                        break;
                    }
                }
            }


            var vacancies1 = from m in _context.vacancies select m;
            if(!string.IsNullOrEmpty(SearchString))
            {
                vacancies1 = vacancies1.Where(s=>s.vacancyname.ToLower().Contains(SearchString.ToLower()) || s.description.ToLower().Contains(SearchString.ToLower()) || s.category.ToLower().Contains(SearchString.ToLower()));  
            }
            vacancies = await vacancies1.ToListAsync();
        }

        public async Task OnPostResponseAsync(int? id)
        {

             Vacancy = await _context.vacancies.FirstOrDefaultAsync(x => x.vacancyId == id);

            if (signinManager.IsSignedIn(User))
            {
                 worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

                Responses response = new Responses()
                {
                    WorkerId = worker.Id,
                    VacancyId = id,
                    OwnerId = Vacancy.userId
                };

                await _context.responses.AddAsync(response);
                _context.SaveChangesAsync();

                responsies = _context.responses.Where(x => x.WorkerId == worker.Id);

                foreach (var vac in responsies)
                {
                    if (vac.VacancyId == Vacancy.vacancyId)
                    {
                        isResponded = true;
                        break;
                    }
                }
            }
            else{ }
        }
    }
}
