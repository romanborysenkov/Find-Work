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


using Microsoft.AspNetCore.Mvc.Rendering;

using System;


namespace FindWorkRazor.Pages.Employer
{
    public class IndexModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signinManager;

        private readonly DataContext _context;

        [BindProperty]
        public WorkerIn Model { get; set; }

        [BindProperty]
        public User employer { get; set; }

        public IndexModel(DataContext context, SignInManager<IdentityUser> signinManager)
        {
            this.signinManager = signinManager;
            _context = context;
        }

        public List<Resume> resumes { get; set; }

        [BindProperty(SupportsGet =true)]
        public string? SearchString { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            if (signinManager.IsSignedIn(User))
                employer = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

            if(!employer.isEmployer)
            {
                return RedirectToPage("../Index");
            }
            else
            {
                var resumes1 = from m in _context.resumes select m;

                if (!string.IsNullOrEmpty(SearchString))
                {
                    resumes1 = resumes1.Where(s => s.desireWork.ToLower().Contains(SearchString.ToLower()));
                }
                resumes = await resumes1.ToListAsync();

                return Page();
            }
        }

        public async Task<IActionResult> OnPostAsync(string? returnUrl=null)
        {
            if (returnUrl == null || returnUrl == "/")
            {
                return RedirectToPage("Index");
            }
            else
            {
                return RedirectToPage(returnUrl);
            }
            return Page();
        }

        public async Task GoToDetails()
        {
            RedirectToPage("LogIn");
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signinManager.SignOutAsync();
            return RedirectToPage("/Index");
        }
    }
}
