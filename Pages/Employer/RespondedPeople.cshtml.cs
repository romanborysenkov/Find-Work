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
    public class RespondedPeopleModel : PageModel
    {

        private readonly SignInManager<IdentityUser> signinManager;


        public User employer { get; set; }

        private readonly DataContext _context;



        public List<Resume> resumes { get; set; } = new List<Resume>();


        public RespondedPeopleModel(DataContext context, SignInManager<IdentityUser> signInManager)
        {
            signinManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (signinManager.IsSignedIn(User))
                employer = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
            else return RedirectToPage("/Index");

            var responcies =await _context.responses.Where(x => x.OwnerId == employer.Id).ToListAsync();

            for (int i = 0; i < responcies.Count; i++)
            {
                resumes.Add(await _context.resumes.FirstAsync(x=>x.userId==responcies[i].WorkerId));
            }

            return Page();

        }
    }
}
