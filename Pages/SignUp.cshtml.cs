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

      //  [BindProperty]
        //public Worker Model { get; set; }

        [BindProperty]
        public User Model { get; set; }

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

                var IdUser = new IdentityUser()
                {
                   UserName = Model.email,
                   Email = Model.email,
                   PhoneNumber = Model.phone,
                };

                var forw = new User()
                {
                    Id = IdUser.Id,
                    firstname = Model.firstname,
                    secondname = Model.secondname,
                    email = Model.email,
                    salt = Model.salt,
                    phone = Model.phone,
                    isEmployer = Model.isEmployer
                };

                var result = await userManager.CreateAsync(IdUser, Model.salt);
                if (result.Succeeded)
                {
                    _context.users.Add(forw);
                    await _context.SaveChangesAsync();
                    await signInManager.SignInAsync(IdUser, false);

                    if (Model.isEmployer) return RedirectToPage("./Employer/Index");
                       else return RedirectToPage("Index");
                    
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
