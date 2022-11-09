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
       public Resume resume { get; set; }

        [BindProperty]
         public User user { get; set; }

        public async Task OnGet()
        {
            user = await _context.users.FirstAsync(s=>s.email==User.Identity.Name);

            try
            {
                resume = await _context.resumes.FirstAsync(s => s.userId == user.Id);
            }
            catch
            {
                if (resume ==null)
                {
                    resume = new Resume();
                }
            }

          //  worker = await _context.workers.FirstAsync(s=>s.secondname==User.Identity.Name);

            // worker.resumeSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, worker.resumeName);
           
           // var profile = from m in _context.workers
           //                 select m;

            //  worker = (Worker)profile.Where(s=>s.firstname==User.Identity.Name);
        }

        public async Task<IActionResult> OnPostAsync()
        {
           var user2 = await _context.users.FirstAsync(s => s.email == User.Identity.Name);

            user.Id = user2.Id;
            user.salt = user2.salt;

            var IdentUser = await userManager.FindByIdAsync(user.Id);

            if (user.email != null)
            {
                IdentUser.Email = user.email;
            }

            if (user.firstname != null)
            {
                IdentUser.UserName = user.email;
              
            }

            if (user.phone != null)
            {
                IdentUser.PhoneNumber = user.phone;
               
            }

            IdentUser.PasswordHash = passwordHasher.HashPassword(IdentUser, user.salt);

            resume.userId = user.Id;


            var result = await userManager.UpdateAsync(IdentUser);

            _context.users.Update(user);

            Resume resume2 = new Resume();
            try
            {
                 resume2 = await _context.resumes.FirstAsync(s => s.userId == user.Id);
            }
            catch { } 

            resume2.userId = user.Id;
            resume2.age = resume.age;
            resume2.desireWork = resume.desireWork;
            resume2.desireSalary = resume.desireSalary;
            resume2.employmentDegree = resume.employmentDegree;
            resume2.education = resume.education;
            resume2.educationDegree = resume.educationDegree;
            resume2.graduationYear = resume.graduationYear;

            resume2.expirience = resume.expirience;
            resume2.publishTime = DateTime.Now;
            resume2.skills = resume.skills;
            resume2.languages = resume.languages;
            resume2.Location = resume.Location;
           

            if (resume.photoFile != null)
            {
                resume2.photoName = await SaveImage(resume.photoFile);
                resume2.photoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, resume2.photoName);
            }




            if (resume2.Id==null)
            {
                _context.resumes.Add(resume2);
                _context.users.Update(user);
            }
            else
            {
              //  resume.Id = _context.resumes.FirstAsync(s => s.userId == user.Id).Id;
                _context.resumes.Update(resume2);
                _context.users.Update(user);
            }

            await _context.SaveChangesAsync();
            //  return RedirectToPage("Index");
            return Page();

            /* var w = await _context.users.FirstAsync(s => s.email == User.Identity.Name);
              var user = await userManager.FindByIdAsync(w.workerId);

              if(worker.workerphone!=null)
              {
                  w.workerphone = worker.workerphone;
              }

              if (worker.email != null)
              {
                  w.email = worker.email;
              }

              if (worker.firstname != null)
              {
                  w.firstname = worker.firstname;
                  w.secondname = worker.secondname;
                  user.UserName = worker.secondname;
              }

              w.age = worker.age;


              w.desirework = worker.desirework;
              w.desiresalary = worker.desiresalary;
              w.employmentDegree = worker.employmentDegree;
              w.education = worker.education;
              w.educationDegree = worker.educationDegree;
              w.graduationYear = worker.graduationYear;
              w.expirience = worker.expirience;
              w.skills = worker.skills;
              w.languages = worker.languages;
              w.Location = worker.Location;


              user.Email = worker.email;
              user.PasswordHash = passwordHasher.HashPassword(user, w.salt);

              user.PhoneNumber = w.workerphone;


              if (worker.photoFile!=null)
              {
                  w.photoName =await SaveImage(worker.photoFile);
                  w.photoSrc = String.Format("{0}://{1}{2}/Images/{3}", Request.Scheme, Request.Host, Request.PathBase, w.photoName);
              }

              var result = await userManager.UpdateAsync(user);

                      _context.workers.Update(w);
                      await _context.SaveChangesAsync();
                    //  return RedirectToPage("Index");
              return Page();

              */
        }

        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await signInManager.SignOutAsync();
            return RedirectToPage("Index");
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
