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
    public class ResumeDetailsModel : PageModel
    {

        private readonly DataContext _context;

        private readonly SignInManager<IdentityUser> signinManager;

        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }

        public List<Vacancy> vacancies { get; set; }

        public ResumeDetailsModel(DataContext context, SignInManager<IdentityUser> signinManager)
        {
            this.signinManager = signinManager;
            _context = context;
        }

        public Resume resume { get; set; }

        public User user { get; set; }

        public User resumeOwner { get; set; } = new User();

        IQueryable<InterviewCall> called { get; set; }

        public bool isCalled { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            user = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

            resume = await _context.resumes.FirstOrDefaultAsync(x => x.Id == id);
            resumeOwner = await _context.users.FirstAsync(x => x.Id == resume.userId);

            called = _context.interviewCalls.Where(x => x.WorkerId == resume.userId);

            foreach (var call in called)
            {
                if (call.WorkerId == resume.userId)
                {
                    isCalled = true;
                    break;
                }
            }
            return Page();
        }



        public async Task OnPostCallAsync(int? id)
        {
            resume = await _context.resumes.FirstOrDefaultAsync(x => x.Id == id);

            if (signinManager.IsSignedIn(User))
            {
                user = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

                InterviewCall interviewcall = new InterviewCall()
                {
                    HRId = user.Id,
                    WorkerId = resume.userId,
                    Called = DateTime.Now
                };

                await _context.interviewCalls.AddAsync(interviewcall);
                _context.SaveChangesAsync();

                called =  _context.interviewCalls.Where(x => x.WorkerId == resume.userId);

                foreach(var call in called)
                {
                    if (call.WorkerId == resume.userId)
                    {
                        isCalled = true;
                        break;
                    }
                }
            }
        }

    }
}
