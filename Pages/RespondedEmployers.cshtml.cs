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
    public class RespondedEmployersModel : PageModel
    {
        private readonly SignInManager<IdentityUser> signinManager;


        public User worker { get; set; }

        private readonly DataContext _context;



        public List<User> HRS { get; set; } = new List<User>();


        public RespondedEmployersModel(DataContext context, SignInManager<IdentityUser> signInManager)
        {
            signinManager = signInManager;
            _context = context;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            if (signinManager.IsSignedIn(User))
                worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
            else return RedirectToPage("/Index");

            var responcies = await _context.interviewCalls.Where(x => x.WorkerId == worker.Id).ToListAsync();

            for (int i = 0; i < responcies.Count; i++)
            {
                HRS.Add(await _context.users.FirstAsync(x => x.Id == responcies[i].HRId));
            }

            return Page();

        }
    }
}
