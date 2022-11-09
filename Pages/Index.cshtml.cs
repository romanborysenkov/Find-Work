using FindWorkRazor.Data;
using FindWorkRazor.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System;

namespace FindWorkRazor.Pages;

public class IndexModel : PageModel
{
    private readonly SignInManager<IdentityUser> signinManager;

    private readonly DataContext _context;

    [BindProperty]
    public WorkerIn Model { get; set; }

    public User worker { get; set; }

    public IndexModel(DataContext context, SignInManager<IdentityUser> signinManager)
    {
        this.signinManager = signinManager;
        _context = context; 
    }
    public List<Vacancy> vacancies { get; set; }

    [BindProperty(SupportsGet =true)]
    public string? SearchString { get; set; }

    public SelectList? Category { get; set; }

    [BindProperty(SupportsGet =true)]
    public string? WorkCategory { get; set; }

    public List<string> CategoryList { get; set; }

    public async Task<IActionResult> OnGetAsync()
    {
        if (signinManager.IsSignedIn(User))
        {
            worker = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
            if (worker.isEmployer)
            {
                return RedirectToPage("./Employer/Index");
            }
        }
        


            var CL = (from m in _context.vacancies select m.category);

            CategoryList = await CL.ToListAsync();

            for (int i = 0; i < CategoryList.Count; ++i)
            {
                for (int r = i + 1; r < CategoryList.Count; ++r)
                {
                    if (CategoryList[i] == CategoryList[r])
                    {
                        CategoryList.RemoveAt(r);
                    }
                }
            }

            // worker = await _context.workers.FirstAsync(s => s.secondname == User.Identity.Name);

            var vacancies1 = from m in _context.vacancies
                             select m;
            if (!string.IsNullOrEmpty(SearchString))
            {
                vacancies1 = vacancies1.Where(s => s.vacancyname.ToLower().Contains(SearchString.ToLower()) || s.description.ToLower().Contains(SearchString.ToLower()) || s.category.ToLower().Contains(SearchString.ToLower()));
            }
            vacancies = await vacancies1.ToListAsync();

            return Page();
        

      //  vacancies = await _context.vacancies.ToListAsync();
    }

    public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
    {
        if (ModelState.IsValid)
        {
                var identityResult = await signinManager.PasswordSignInAsync(Model.name, Model.salt, true, false);       

                if(!identityResult.Succeeded)
                {
                ModelState.TryAddModelError("", "Username or Password incorrect");
             }
            
           
            // if(identityResult.Succeeded)
            {
                if (returnUrl == null || returnUrl == "/")
                {
                    return RedirectToPage("Index");
                }
                else
                {
                    return RedirectToPage(returnUrl);
                }
            }
            ModelState.AddModelError("", "Username or Password incorrect");
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
        return RedirectToPage("Index");
    }
}