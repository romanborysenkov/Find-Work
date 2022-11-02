using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace FindWorkRazor.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private DataContext _context;

        [BindProperty]
        public Worker Model { get; set; }

        public SignUpModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, DataContext context)
        {
            _context = context;
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {

                var worker = new IdentityUser()
                {
                   UserName = Model.secondname,
                   Email = Model.email,
                   PhoneNumber = Model.workerphone,
                };

                var forw = new Worker()
                {
                    workerId = worker.Id,
                    firstname = Model.firstname,
                    secondname = Model.secondname,
                    email = Model.email,
                    salt = Model.salt,
                    workerphone = Model.workerphone,
                    age = Model.age
                };

                var result = await userManager.CreateAsync(worker, Model.salt);
                if (result.Succeeded)
                {
                    _context.workers.Add(forw);
                    await _context.SaveChangesAsync();
                    await signInManager.SignInAsync(worker, false);
                    return RedirectToPage("Index");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return Page();
        }
    }
}
