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
using Microsoft.Extensions.Hosting;

namespace FindWorkRazor.Pages
{
    public class ProfileModel : PageModel
    {
        public DataContext _context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IPasswordHasher<IdentityUser> passwordHasher;
        private readonly IWebHostEnvironment _hostEnvironment;

        public ProfileModel(IPasswordHasher<IdentityUser> passwordHash, UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager, DataContext context, IWebHostEnvironment hostEnvironment)
        {
            _hostEnvironment = hostEnvironment;
            passwordHasher = passwordHash;
            this.userManager = userManager;
            this.signInManager = signInManager;
            _context = context;
        }

        [BindProperty]
       public Worker worker { get; set; }

        public async Task OnGet()
        {
            worker = await _context.workers.FirstAsync(s=>s.secondname==User.Identity.Name);
            // worker.resumeSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, worker.resumeName);
           
           // var profile = from m in _context.workers
           //                 select m;

            //  worker = (Worker)profile.Where(s=>s.firstname==User.Identity.Name);
        }

        public async Task<IActionResult> OnPostAsync()
        {
           var w = await _context.workers.FirstAsync(s => s.secondname == User.Identity.Name);
            var user = await userManager.FindByIdAsync(w.workerId);

            if(worker.workerphone!=null)
            {
                w.workerphone = worker.workerphone;
            }

            if (worker.email != null)
            {
                w.email = worker.email;
            }
            w.desirework = worker.desirework;
            w.desiresalary = worker.desiresalary;
            w.skills = worker.skills;
            w.education = worker.education;
            w.expirience = worker.expirience;
            w.languages = worker.languages;
            w.Location = worker.Location;

            user.Email = w.email;
            user.PasswordHash = passwordHasher.HashPassword(user, w.salt);
            user.UserName = User.Identity.Name;
            user.PhoneNumber = w.workerphone;


            if (worker.resumeFile!=null)
            {
                w.resumeName =await SaveImage(worker.resumeFile);
                w.resumeSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, w.resumeName);
            }

            var result = await userManager.UpdateAsync(user);
           
                    _context.workers.Update(w);
                    await _context.SaveChangesAsync();
                    return RedirectToPage("Index");
            return Page();
        }


        [NonAction]
        public async Task<string> SaveImage(IFormFile imageFile)
        {
            string imageName = new String(Path.GetFileNameWithoutExtension(imageFile.FileName).Take(10).ToArray()).Replace(' ', '-');
            imageName = imageName + Path.GetExtension(imageFile.FileName);
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            using (var fileStream = new FileStream(imagePath, FileMode.Create))
            {
                await imageFile.CopyToAsync(fileStream);
            }
            return imageName;
        }

        [NonAction]
        public void DeleteImage(string imageName)
        {
            var imagePath = Path.Combine(_hostEnvironment.ContentRootPath, "Images", imageName);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);

        }
    }
}
